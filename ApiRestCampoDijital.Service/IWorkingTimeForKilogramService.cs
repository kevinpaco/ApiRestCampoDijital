using ApiRestCampoDijital.Model.workingTime;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Service
{
    public interface IWorkingTimeForKilogramService:IWorkingTimeService<WorkingTimeDTO>
    {
        Task<List<WorkingTimeDTO>> ListWorkingTimeInGroup(int idWorkingInGroup, int idFarm);
    }
}
