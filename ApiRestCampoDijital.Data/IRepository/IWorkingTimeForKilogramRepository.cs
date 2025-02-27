using ApiRestCampoDijital.Model.workingTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.IRepository
{
    public interface IWorkingTimeForKilogramRepository : IWorkingTimeRepository<WorkingTimeForKilogram>
    {
        Task<List<WorkingTimeForKilogram>> ListWorkingTimeInGroup(int idWorkingInGroup, int idFarm);
    }
}
