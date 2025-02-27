using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.Repository
{
    public class FarmSupervisorRepository : IFarmSupervisorRepository
    {
        public async Task<bool> AddFarmSupervisor(FarmSupervisor farmSupervisor)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    await efc.farmSupervisors.AddAsync(farmSupervisor);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar supervisor en base de datos: " + ex.InnerException);
            }
        }

        public async Task<FarmSupervisor> FindFarmSupervisorByDni(int dni)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    FarmSupervisor farmSupervisor =await efc.farmSupervisors.FirstOrDefaultAsync(f => f.dniSupervisor==dni);
                    if (farmSupervisor == null)
                        return null;

                    return farmSupervisor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar supervisor en base de datos: " + ex.InnerException);
            }
        }
    }
}
