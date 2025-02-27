using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model.workingTime;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Service
{
    public interface IWorkingTimeInGroupService
    {
        Task<bool> AddWorkingTimeInGroup(WorkingTimeInGroupDTO workingTimeInGroup);
        Task<bool> UpdateWorkingTimeInGroup(WorkingTimeInGroupDTO workingTimeInGroupDTO);
        Task<WorkingTimeInGroupDTO> FindWorkingTimeInGroupById(int idWorking, int idFarm);
        Task<PaginatedListting<WorkingTimeInGroupDTO>> GetAllWorkingTimeInGroup(DateTime dateTime, int idFarm);
    }
}
