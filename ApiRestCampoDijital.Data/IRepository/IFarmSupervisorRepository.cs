using ApiRestCampoDijital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.IRepository
{
    public interface IFarmSupervisorRepository
    {
        Task<bool> AddFarmSupervisor(FarmSupervisor farmSupervisor);
        Task<FarmSupervisor> FindFarmSupervisorByDni(int dni);
    }
}
