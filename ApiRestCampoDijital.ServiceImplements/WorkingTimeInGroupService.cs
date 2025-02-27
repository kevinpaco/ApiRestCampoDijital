using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model.workingTime;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using ApiRestCampoDijital.ServiceImplements.Layout;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements
{
    public class WorkingTimeInGroupService : IWorkingTimeInGroupService
    {
        private readonly IWorkingTimeInGroupRepository workingTimeInGroupRepository;
        private readonly IWorkingTimeForKilogramService workingTimeForKilogramService;
        private readonly IEmployeeService employeeService;
        private readonly IHistoryService historyService;
        public WorkingTimeInGroupService(IWorkingTimeInGroupRepository workingTimeInGroupRepository, 
                                         IWorkingTimeForKilogramService workingTimeForKilogramService,
                                         IEmployeeService employeeService,
                                         IHistoryService historyService)
        {
            this.workingTimeInGroupRepository = workingTimeInGroupRepository;
            this.workingTimeForKilogramService = workingTimeForKilogramService;
            this.employeeService = employeeService;
            this.historyService = historyService;
        }
        #region save WorkingTimeInGroup
        public async Task<bool> AddWorkingTimeInGroup(WorkingTimeInGroupDTO workingTimeInGroupDTO)
        {
            if (workingTimeInGroupDTO.WorkingTimes.Count == 0)
                throw new ArgumentNullException("Debe Ingresar los datos minimos de la jornada por kilo");
          
            _= await ValidatedEmployeeExisting(workingTimeInGroupDTO.WorkingTimes, workingTimeInGroupDTO.farmId);
            
            WorkingTimeInGroup workingTimeInGroup = WorkingTimeInGroupMapper.GetWorkingTimeInGroup(workingTimeInGroupDTO);
            addHistory($"{workingTimeInGroup.AttendantName} agrego una nueva jornada por grupo de cinta o clasificadores");
            //se guarda el trabajo en grupo
            int idWorkingTimeInGroupSave =  await this.workingTimeInGroupRepository.AddWorkingTimeInGroup(workingTimeInGroup);
            return await CreateWorkingTimeForKilogram(workingTimeInGroupDTO,idWorkingTimeInGroupSave);
            
        }

        private async Task<bool> CreateWorkingTimeForKilogram(WorkingTimeInGroupDTO workingTimeInGroupDTO,int idWokingInGroupSave)
        {
           // throw new Exception("erer: " + workingTimeInGroupDTO.Date);
            var employeeKilograms =workingTimeInGroupDTO.TotalKilograms/ workingTimeInGroupDTO.EmployeePresent;
            foreach (var workingTime in workingTimeInGroupDTO.WorkingTimes)
            {
                if(workingTime.Present)
                       workingTime.KilogramsNumber = employeeKilograms;
                workingTime.Date = workingTimeInGroupDTO.Date;
                workingTime.AttendantName = workingTimeInGroupDTO.AttendantName;
                workingTime.Category = workingTimeInGroupDTO.CategoryGroup;
                workingTime.FarmId = workingTimeInGroupDTO.farmId;
                workingTime.WorkingTimeInGroupId = idWokingInGroupSave;
                await this.workingTimeForKilogramService.AddWorkingTime(workingTime);
            }
            return true;
        }

        private async Task<bool> ValidatedEmployeeExisting(List<WorkingTimeDTO> workingTimeDTOs,int idFarm)
        {
        
             foreach (var workingTime in workingTimeDTOs)
            {
                EmployeeDTO employeeDTO = this.employeeService.FindEmployeeById(workingTime.EmployeeId).Result;
                var existingEmployeeInFarm = await this.employeeService.FindEmployeeByDni(employeeDTO.Dni, idFarm);
            }
            return true;
        }
        #endregion

        #region update WorkingTimeInGroup
        public async Task<bool> UpdateWorkingTimeInGroup(WorkingTimeInGroupDTO workingTimeInGroupDTO)
        {
            if (workingTimeInGroupDTO.WorkingTimes.Count == 0)
                throw new ArgumentNullException("Debe Ingresar los datos minimos de la jornada por kilo");

            _ = await ValidatedEmployeeExisting(workingTimeInGroupDTO.WorkingTimes, workingTimeInGroupDTO.farmId);

            WorkingTimeInGroup workingTimeInGroup = WorkingTimeInGroupMapper.GetWorkingTimeInGroup(workingTimeInGroupDTO);

            int idWorkingTimeInGroupSave = await this.workingTimeInGroupRepository.UpdateWorkingTimeInGroup(workingTimeInGroup);
            return await UpdateWorkingTimeForKilogram(workingTimeInGroupDTO, idWorkingTimeInGroupSave);

        }

        private async Task<bool> UpdateWorkingTimeForKilogram(WorkingTimeInGroupDTO workingTimeInGroupDTO, int idWokingInGroupSave)
        {
            // throw new Exception("erer: " + workingTimeInGroupDTO.Date);
            var employeeKilograms = workingTimeInGroupDTO.TotalKilograms / workingTimeInGroupDTO.EmployeePresent;
            foreach (var workingTime in workingTimeInGroupDTO.WorkingTimes)
            {
                if (workingTime.Present)
                    workingTime.KilogramsNumber = employeeKilograms;
                else
                    workingTime.KilogramsNumber = 0;
                workingTime.Date = workingTimeInGroupDTO.Date;
                workingTime.AttendantName = workingTimeInGroupDTO.AttendantName;
                workingTime.Category = workingTimeInGroupDTO.CategoryGroup;
                workingTime.FarmId = workingTimeInGroupDTO.farmId;
                workingTime.WorkingTimeInGroupId = idWokingInGroupSave;
                await this.workingTimeForKilogramService.UpdateWorkingTime(workingTime);
            }
            return true;
        }
        #endregion

        public async Task<WorkingTimeInGroupDTO> FindWorkingTimeInGroupById(int idWorking, int idFarm)
        {
            WorkingTimeInGroup workingTimeInGroup= await this.workingTimeInGroupRepository.FindWorkingTimeInGroupById(idWorking, idFarm);
            WorkingTimeInGroupDTO workingTimeInGroupDTO = WorkingTimeInGroupMapper.GetWorkingTimeInGroupDTO(workingTimeInGroup);
            return workingTimeInGroupDTO;
        }

        #region List All WorkingTimeInGroup
        public async Task<PaginatedListting<WorkingTimeInGroupDTO>> GetAllWorkingTimeInGroup(DateTime dateTime, int idFarm)
        {
            PaginatedListting<WorkingTimeInGroup> paginatedListting =await this.workingTimeInGroupRepository.GetAllWorkingTimeInGroup(dateTime,idFarm);
            var paginatedListtingDTO = new PaginatedListting<WorkingTimeInGroupDTO>();
            paginatedListtingDTO.count = paginatedListting.count;
            paginatedListtingDTO.list = WorkingTimeInGroupMapper.GetListtWorkingTimeInGroup(paginatedListting.list);
            AddWorkingForKilogramTOWorkingInGroup(paginatedListtingDTO.list); 
            return paginatedListtingDTO;
        }

        private async void AddWorkingForKilogramTOWorkingInGroup(List<WorkingTimeInGroupDTO> workingTimeInGroupDTOs)
        {    
            foreach(var workingTimeInGroup in workingTimeInGroupDTOs)
            {
                workingTimeInGroup.WorkingTimes =await this.workingTimeForKilogramService.ListWorkingTimeInGroup(workingTimeInGroup.Id,workingTimeInGroup.farmId);
            }
        }
        #endregion

        private void addHistory(string description)
        {
            HistoryDTO historyDTO = new HistoryDTO()
            {
                Description = description,
            };
            this.historyService.AddHistory(historyDTO);
        }
    }
}
