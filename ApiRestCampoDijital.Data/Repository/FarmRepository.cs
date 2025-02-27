using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.Repository
{
    public class FarmRepository : IFarmRepository
    {
        public async Task<bool> AddFarm(Farm farm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                   await efc.farms.AddAsync(farm);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar finca en base de datos: " + ex.InnerException);
            }
        }

        public async Task<bool> DeleteFarmByID(Farm farm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    efc.farms.Update(farm);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar finca en base de datos: " + ex.InnerException);
            }
        }

        public async Task<Farm> FindFarmById(int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    Farm? farm = new Farm();
                    farm   = await efc.farms.Where(f => !f.Deleted && f.Id==idFarm).Include(f=> f.Employer).FirstOrDefaultAsync();
                   
                    
                    return farm;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar finca en base de datos: " + ex.InnerException);
            }
        }

        public async Task<PaginatedListting<Farm>> FindFarmByIdEmployer(int idEmployer)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var query = await efc.farms.Where(f => !f.Deleted && f.EmployerId == idEmployer).Include(f=> f.Employer).ToListAsync();
                    query = query.OrderBy(f => f.Id).ToList();
                    PaginatedListting<Farm> paginatedListting = new PaginatedListting<Farm>()
                    {
                        count = query.Count,
                        list = query
                    };                    
                    return paginatedListting;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error buscar fincas en base de datos: " + ex.InnerException);
            }
        }
    }
}
