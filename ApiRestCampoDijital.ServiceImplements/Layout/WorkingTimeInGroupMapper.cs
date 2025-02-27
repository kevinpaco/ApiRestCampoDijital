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
    public class WorkingTimeInGroupMapper
    {
        public static WorkingTimeInGroup GetWorkingTimeInGroup(WorkingTimeInGroupDTO workingTimeInGroupDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkingTimeInGroupDTO, WorkingTimeInGroup>()
                                                  .ForMember(w => w.WorkingTimes,otp => otp.Ignore()));

            var mapper = config.CreateMapper();
            WorkingTimeInGroup workingTimeInGroup = mapper.Map<WorkingTimeInGroup>(workingTimeInGroupDTO);
         //   workingTimeInGroup.WorkingTimes = ConvertDTOToWorkingTimeForKilogram(workingTimeInGroupDTO.WorkingTimes);
            return workingTimeInGroup;
        }

       /* public static List<WorkingTimeForKilogram> ConvertDTOToWorkingTimeForKilogram(List<WorkingTimeDTO> workingTimeDTOs)
        {
            List<WorkingTimeForKilogram> workingTimeForKilograms = new List<WorkingTimeForKilogram>();
            foreach (var dto in workingTimeDTOs)
            {
                workingTimeForKilograms.Add(WorkingTimeMapper.GetWorkingTimeKilogram(dto));
            }
            return workingTimeForKilograms;
        }*/

        public static WorkingTimeInGroupDTO GetWorkingTimeInGroupDTO(WorkingTimeInGroup workingTimeInGroup)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WorkingTimeInGroup, WorkingTimeInGroupDTO>()
                                                  .ForMember(w => w.WorkingTimes, otp => otp.Ignore()));

            var mapper = config.CreateMapper();
            WorkingTimeInGroupDTO workingTimeInGroupDto = mapper.Map<WorkingTimeInGroupDTO>(workingTimeInGroup);
            return workingTimeInGroupDto;
        }

        public static List<WorkingTimeInGroupDTO> GetListtWorkingTimeInGroup(List<WorkingTimeInGroup> workingTimeInGroups)
        {
            var listWorkingDto = new List<WorkingTimeInGroupDTO>();

            foreach (var workingTime in workingTimeInGroups)
            {
                var workingDTO = new WorkingTimeInGroupDTO();
                workingDTO = GetWorkingTimeInGroupDTO(workingTime);
               // workingDTO.WorkingTimes = WorkingTimeMapper.GetListWorkingTimeForKilogramDTO(workingTime.WorkingTimes);
                listWorkingDto.Add(workingDTO);
            }
            return listWorkingDto;
        }
    }
}
