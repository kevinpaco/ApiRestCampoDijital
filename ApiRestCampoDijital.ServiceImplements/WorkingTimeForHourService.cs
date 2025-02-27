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
    public class WorkingTimeForHourService : IWorkingTimeForHourService
    {
        private readonly IWorkingTimeForHourRepository workingTimeForHourRepository;
        private readonly IHistoryService historyService;
        private readonly IEmployeeService employeeService;

        public WorkingTimeForHourService(IWorkingTimeForHourRepository workingTimeForHourRepository,
                                         IHistoryService historyService,IEmployeeService employeeService)
        {
            this.workingTimeForHourRepository = workingTimeForHourRepository;
            this.historyService = historyService;
            this.employeeService = employeeService;
        }
        public Task<bool> AddWorkingTime(WorkingTimeDTO workingTimeDTO)
        {
            WorkingTimeForHour workingTimeForHour = WorkingTimeMapper.GetWorkingTime(workingTimeDTO);
            addHistory($"{workingTimeForHour.AttendantName} agrego una nueva jornada por hora");

            return this.workingTimeForHourRepository.AddWorkingTime(workingTimeForHour);
        }

        public async Task<WorkingTimeDTO> FindWorkingTimeById(int idWorkingTime)
        {
            WorkingTimeForHour workingTimeForHour =await this.workingTimeForHourRepository.FindWorkingTimeById(idWorkingTime);   
            WorkingTimeDTO workingTimeDTO = WorkingTimeMapper.GetWorkingTimeDTO(workingTimeForHour);
            return workingTimeDTO;
        }

        public async Task<PaginatedListting<WorkingTimeDTO>> ListWorkingTime(DateTime dateTime, int idFarm)
        {
            PaginatedListting<WorkingTimeForHour> paginatedListting = await this.workingTimeForHourRepository.ListWorkingTime(dateTime,idFarm);
            PaginatedListting<WorkingTimeDTO> paginatedListtingDTO = new PaginatedListting<WorkingTimeDTO>();
            
            paginatedListtingDTO.count = paginatedListting.count;
            paginatedListtingDTO.list = WorkingTimeMapper.GetListWorkingTimeForHoutDTO(paginatedListting.list);
           // throw new Exception("ess: " + paginatedListting.list.Count+"/"+d.Count());
            return paginatedListtingDTO;
        }

        public async Task<PaginatedListting<WorkingTimeDTO>> ListWorkingTimeNotPaid(FilterWorkingTimeDTO filterWorkingTimeDTO, int idFarm)
        {
            FilterWorkingTime filterWorkingTime = FilterWorkingTimeMapper.GetFilterWorkingTime(filterWorkingTimeDTO);

            EmployeeDTO employeeDTO = this.employeeService.FindEmployeeByDni(filterWorkingTimeDTO.EmployerDNI, idFarm).Result;
            filterWorkingTime.EmployeeId = employeeDTO.Id; 

            PaginatedListting<WorkingTimeForHour> paginatedListting = await this.workingTimeForHourRepository.ListWorkingTimeNotPaid(filterWorkingTime,idFarm);
            PaginatedListting<WorkingTimeDTO> paginatedListtingDTO = new PaginatedListting<WorkingTimeDTO>();

            paginatedListtingDTO.count = paginatedListting.count;
            paginatedListtingDTO.list = WorkingTimeMapper.GetListWorkingTimeForHoutDTO(paginatedListting.list);
            // throw new Exception("ess: " + paginatedListting.list.Count+"/"+d.Count());
            return paginatedListtingDTO;
        }

        public Task<bool> UpdateWorkingTime(WorkingTimeDTO workingTimeDTO)
        {
            WorkingTimeForHour workingTimeForHour = WorkingTimeMapper.GetWorkingTime(workingTimeDTO);
            addHistory($"{workingTimeForHour.AttendantName} actualizo una jornada por hora, con id: {workingTimeForHour.Id}, y empleado: {workingTimeForHour.EmployeeId}");
            return this.workingTimeForHourRepository.UpdateWorkingTime(workingTimeForHour);
        }

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
