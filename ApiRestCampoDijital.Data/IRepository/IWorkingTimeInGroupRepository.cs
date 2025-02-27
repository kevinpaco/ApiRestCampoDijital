using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model.workingTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.IRepository
{
    public interface IWorkingTimeInGroupRepository
    {
        Task<int> AddWorkingTimeInGroup(WorkingTimeInGroup workingTimeInGroup);
        Task<int> UpdateWorkingTimeInGroup(WorkingTimeInGroup workingTimeInGroup);
        Task<WorkingTimeInGroup> FindWorkingTimeInGroupById(int idWorking,int idFarm);
        Task<PaginatedListting<WorkingTimeInGroup>> GetAllWorkingTimeInGroup(DateTime dateTime,int idFarm);

        
    }
}
