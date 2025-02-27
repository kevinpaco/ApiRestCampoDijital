using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.Repository
{
    public class EmployerRepository: IEmployerRepository
    {
        public async Task<bool> AddEmployer(Employer employer)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    await efc.employers.AddAsync(employer);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) {
                throw new Exception("Error al agregar employer en base de datos: "+ex.Message);
            }
        }

        public async Task<bool> DeleteEmployer(long employerId)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    Employer? employer =await efc.employers.Where(e => !e.Deleted && e.Id == employerId).FirstOrDefaultAsync();
                    if (employer != null) { 
                        employer.Deleted = true;
                        efc.employers.Update(employer);
                        await efc.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Error: el Empleador a eliminar no existe");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar employer en base de datos: " + ex.Message);
            }
        }

        public async Task<Employer> EmployerAutentication(long cuit)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    Employer? employer = await efc.employers.FirstOrDefaultAsync(e => !e.Deleted && e.Cuit==cuit);
                    if (employer != null)
                    {
                        return employer;
                    }
                    else
                    {
                        throw new Exception("Error: el Empleador no existe");
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar employer en base de datos: " + ex.InnerException);
            }
        }

        public async Task<bool> EmployerModify(Employer employer)
        {
            try
            {
                using (var efc = new EfContext())
                {                   
                    efc.employers.Update(employer);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar employer en base de datos: " + ex.Message);
            }
        }

        public async Task<Employer> FindEmployerById(int employerId)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    Employer? employer =await efc.employers.Where(e => !e.Deleted && e.Id == employerId).FirstOrDefaultAsync();
                    if (employer != null)
                    {
                        return employer;
                    }
                    else
                    {
                        throw new Exception("El empleador no existe en base de datos");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar empleador en base de datos: "+ex.Message);
            }
        }

        public async Task<Employer> FindEmployerByCuit(long cuit)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    Employer? employer = await efc.employers.Where(e => !e.Deleted && e.Cuit == cuit).FirstOrDefaultAsync();
                    if (employer != null)
                    {
                        return employer;
                    }
                    else
                    {
                        throw new Exception("El empleador no existe en base de datos");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar empleador en base de datos: " + ex.Message);
            }
        }

        public Task<PaginatedListting<Employer>> ListEmployers(long cuit)
        {
            try
            {
                using(var efc= new EfContext())
                {
                    PaginatedListting<Employer> paginatedListting = new PaginatedListting<Employer>();
                    var query = efc.employers.Where(e => !e.Deleted);
                    if (cuit != 0)
                    {
                        query = query.Where(e => e.Cuit == cuit);
                    }
                    paginatedListting.count = query.Count();
                    paginatedListting.list = query.OrderBy(e=>e.Id).ToList();
                    return Task.FromResult(paginatedListting);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar emploadores de base de datos: "+ex.Message);
            }
        }
    }
}
