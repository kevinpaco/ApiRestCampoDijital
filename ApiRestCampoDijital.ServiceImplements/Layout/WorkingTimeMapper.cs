using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model.workingTime;
using ApiRestCampoDijital.ModelDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    public class WorkingTimeMapper
    {
        public static WorkingTimeForHour GetWorkingTime(WorkingTimeDTO workingTimeDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkingTimeDTO,WorkingTimeForHour>()
                                                 .ForMember( w => w.Employee,opt => opt.Ignore()));

            var mapper = config.CreateMapper();
            WorkingTimeForHour workingTime = mapper.Map<WorkingTimeForHour>(workingTimeDTO);
            return workingTime;
        }

        public static WorkingTimeDTO GetWorkingTimeDTO(WorkingTimeForHour workingTimeForHour)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkingTimeForHour,WorkingTimeDTO>()
                                                 .ForMember(w => w.Employee, opt => opt.Ignore()));

            var mapper = config.CreateMapper();
            WorkingTimeDTO workingTime = mapper.Map<WorkingTimeDTO>(workingTimeForHour);
            return workingTime;
        }

        public static WorkingTimeDTO GetWorkingTimeForKilogramDTO(WorkingTimeForKilogram workingTimeForKilogram)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkingTimeForKilogram, WorkingTimeDTO>()
                                                 .ForMember(w => w.Employee, opt => opt.Ignore()));

            var mapper = config.CreateMapper();
            WorkingTimeDTO workingTime = mapper.Map<WorkingTimeDTO>(workingTimeForKilogram);
            return workingTime;
        }

        public static WorkingTimeForKilogram GetWorkingTimeKilogram(WorkingTimeDTO workingTimeDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkingTimeDTO, WorkingTimeForKilogram>()
                                                 .ForMember(w => w.Employee, opt => opt.Ignore()));

            var mapper = config.CreateMapper();
            WorkingTimeForKilogram workingTime = mapper.Map<WorkingTimeForKilogram>(workingTimeDTO);
            return workingTime;
        }

        public static List<WorkingTimeDTO> GetListWorkingTimeForHoutDTO(List<WorkingTimeForHour> workingTimes)
        {
            List<WorkingTimeDTO> workingTimeDTOs = new List<WorkingTimeDTO>();
            foreach (var workingTime in workingTimes)
            {   
                WorkingTimeDTO workingTimeDTO = new WorkingTimeDTO()
                {
                    AttendantName = workingTime.AttendantName,
                    Category = workingTime.Category,
                    Date = workingTime.Date,
                    EmployeeId = workingTime.EmployeeId,
                    Employee = EmployeeMapper.GetEmployeeDTO(workingTime.Employee),
                    Id = workingTime.Id,
                    FarmId = workingTime.FarmId,
                    Paid = workingTime.Paid,
                    Present = workingTime.Present,
                    HourNumber = workingTime.HourNumber,
                };
               workingTimeDTOs.Add(workingTimeDTO);
             }
            return workingTimeDTOs;
        }

        public static List<WorkingTimeDTO> GetListWorkingTimeForKilogramDTO(List<WorkingTimeForKilogram> workingTimes)
        {
            List<WorkingTimeDTO> workingTimeDTOs = new List<WorkingTimeDTO>();
            foreach (var workingTime in workingTimes)
            {
                WorkingTimeDTO workingTimeDTO = new WorkingTimeDTO()
                {
                    AttendantName = workingTime.AttendantName,
                    Category = workingTime.Category,
                    Date = workingTime.Date,
                    EmployeeId = workingTime.EmployeeId,
                    Employee = EmployeeMapper.GetEmployeeDTO(workingTime.Employee),
                    Id = workingTime.Id,
                    FarmId = workingTime.FarmId,
                    Paid = workingTime.Paid,
                    Present = workingTime.Present,
                    KilogramsNumber = workingTime.KilogramsNumber,
                    WorkingTimeGroupID = workingTime.WorkingTimeGroupID,
                };
                workingTimeDTOs.Add(workingTimeDTO);
            }
            return workingTimeDTOs;
        }
    }
}
