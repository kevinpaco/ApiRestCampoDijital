using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model.workingTime;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using ApiRestCampoDijital.ServiceImplements.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements
{
    public class WorkingTimeForKilogramService : IWorkingTimeForKilogramService
    {
        private readonly IWorkingTimeForKilogramRepository workingTimeForKilogramRepository;
        private readonly IHistoryService historyService;
        private readonly IEmployeeService employeeService;

        public WorkingTimeForKilogramService(IWorkingTimeForKilogramRepository workingTimeForKilogramRepository,
                                             IHistoryService historyService,
                                             IEmployeeService employeeService)
        {
            this.workingTimeForKilogramRepository = workingTimeForKilogramRepository;
            this.historyService = historyService;
            this.employeeService = employeeService;
        }
        public Task<bool> AddWorkingTime(WorkingTimeDTO workingTimeDTO)
        {
            WorkingTimeForKilogram workingTimeForKilogram = WorkingTimeMapper.GetWorkingTimeKilogram(workingTimeDTO);
            addHistory($"{workingTimeForKilogram.AttendantName} agrego una nueva jornada por Kilogramo");
            return this.workingTimeForKilogramRepository.AddWorkingTime(workingTimeForKilogram);
        }

        public async Task<WorkingTimeDTO> FindWorkingTimeById(int idWorkingTime)
        {
           WorkingTimeForKilogram workingTimeForKilogram = await this.workingTimeForKilogramRepository.FindWorkingTimeById(idWorkingTime);
            WorkingTimeDTO workingTimeDTO = WorkingTimeMapper.GetWorkingTimeForKilogramDTO(workingTimeForKilogram);
            return workingTimeDTO; 
        }

        public async Task<PaginatedListting<WorkingTimeDTO>> ListWorkingTime(DateTime dateTime, int idFarm)
        {
            PaginatedListting<WorkingTimeForKilogram> paginatedListting = await this.workingTimeForKilogramRepository.ListWorkingTime(dateTime, idFarm);
            PaginatedListting<WorkingTimeDTO> paginatedListtingDTO = new PaginatedListting<WorkingTimeDTO>();

            paginatedListtingDTO.count = paginatedListting.count;
            paginatedListtingDTO.list = WorkingTimeMapper.GetListWorkingTimeForKilogramDTO(paginatedListting.list);
            // throw new Exception("ess: " + paginatedListting.list.Count+"/"+d.Count());
            return paginatedListtingDTO;

        }

        public Task<bool> UpdateWorkingTime(WorkingTimeDTO workingTimeDTO)
        {
            WorkingTimeForKilogram workingTimeForKilogram = WorkingTimeMapper.GetWorkingTimeKilogram(workingTimeDTO);
            addHistory($"{workingTimeForKilogram.AttendantName} actualizo una nueva jornada por kilogramo con id: {workingTimeForKilogram.Id}");
            return this.workingTimeForKilogramRepository.UpdateWorkingTime(workingTimeForKilogram);
        }

        public async Task<List<WorkingTimeDTO>> ListWorkingTimeInGroup(int idWorkingInGroup, int idFarm)
        {
            List<WorkingTimeForKilogram> listWorkingForKilogram =await this.workingTimeForKilogramRepository.ListWorkingTimeInGroup(idWorkingInGroup, idFarm);
            List<WorkingTimeDTO> workingTimeDTOs = WorkingTimeMapper.GetListWorkingTimeForKilogramDTO(listWorkingForKilogram);
            return workingTimeDTOs;
        }

        private void addHistory(string description)
        {
            HistoryDTO historyDTO = new HistoryDTO()
            {
                Description = description,
            };
            this.historyService.AddHistory(historyDTO);
        }

        public async Task<PaginatedListting<WorkingTimeDTO>> ListWorkingTimeNotPaid(FilterWorkingTimeDTO filterWorkingTimeDTO, int idFarm)
        {
            FilterWorkingTime filterWorkingTime = FilterWorkingTimeMapper.GetFilterWorkingTime(filterWorkingTimeDTO);

            EmployeeDTO employeeDTO = this.employeeService.FindEmployeeByDni(filterWorkingTimeDTO.EmployerDNI, idFarm).Result;
            filterWorkingTime.EmployeeId = employeeDTO.Id;

            PaginatedListting<WorkingTimeForKilogram> paginatedListting = await this.workingTimeForKilogramRepository.ListWorkingTimeNotPaid(filterWorkingTime, idFarm);
            PaginatedListting<WorkingTimeDTO> paginatedListtingDTO = new PaginatedListting<WorkingTimeDTO>();

            paginatedListtingDTO.count = paginatedListting.count;
            paginatedListtingDTO.list = WorkingTimeMapper.GetListWorkingTimeForKilogramDTO(paginatedListting.list);
            // throw new Exception("ess: " + paginatedListting.list.Count+"/"+d.Count());
            return paginatedListtingDTO;
        }
    }
}
