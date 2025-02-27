using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model.workingTime;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.Repository
{
    public class WorkingTimeForKilogramRepository : IWorkingTimeForKilogramRepository
    {
        public async Task<bool> AddWorkingTime(WorkingTimeForKilogram workingTime)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    await efc.workingTimeForKilograms.AddAsync(workingTime);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar jornada laboral en base de datos: " + ex.InnerException);
            }
        }

        public async Task<WorkingTimeForKilogram> FindWorkingTimeById(int idWorkingTime)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var workingTime = await efc.workingTimeForKilograms.FirstOrDefaultAsync(w => w.Id == idWorkingTime);
                    if (workingTime != null)
                        return workingTime;
                    else
                        throw new Exception("Erro al buscar jornada laboral en base de datos: ");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar jornada laboral en base de datos: " + ex.Message);
            }
        }

        public async Task<PaginatedListting<WorkingTimeForKilogram>> ListWorkingTime(DateTime dateTime, int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var workingTimeForKilograms = efc.Set<WorkingTimeForKilogram>().Where(w => w.FarmId == idFarm);
                     // throw new ArgumentNullException("ess: " + dateTime + "/" + idFarm);
                    if (!dateTime.Equals(new DateTime(2000, 01, 01)))
                    {
                        workingTimeForKilograms = workingTimeForKilograms.Where(w => w.Date.Equals(dateTime));
                    }
                    workingTimeForKilograms = workingTimeForKilograms.Include(w => w.Employee);
                    PaginatedListting<WorkingTimeForKilogram> paginatedListting = new PaginatedListting<WorkingTimeForKilogram>();
                    paginatedListting.count = workingTimeForKilograms.Count();
                    paginatedListting.list = workingTimeForKilograms.ToList();
                    return paginatedListting;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar jornada laboral de base de datos: "+dateTime+"/"+idFarm+"/" + ex.Message);
            }
        }

        public async Task<List<WorkingTimeForKilogram>> ListWorkingTimeInGroup(int idWorkingInGroup, int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var workingTimeForKilograms = efc.Set<WorkingTimeForKilogram>().Where(w => w.FarmId == idFarm && w.WorkingTimeInGroupId==idWorkingInGroup);
                    // throw new ArgumentNullException("ess: " + dateTime + "/" + idFarm);
                   
                    workingTimeForKilograms = workingTimeForKilograms.Include(w => w.Employee).OrderBy(w => w.Date);
                    List<WorkingTimeForKilogram> listWorking = new List<WorkingTimeForKilogram>();
                    listWorking = workingTimeForKilograms.ToList();
                    return listWorking;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar Jornadas por kilo en grupo: "+ ex.Message);
            }
        }

        public async Task<PaginatedListting<WorkingTimeForKilogram>> ListWorkingTimeNotPaid(FilterWorkingTime filterWorkingTime, int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var workingTimeForKilograms = efc.Set<WorkingTimeForKilogram>().Where(w => w.FarmId == idFarm && w.Paid == false && w.EmployeeId == filterWorkingTime.EmployeeId);
                    //  throw new Exception("ess: " + dateTime + "/" + idFarm);

                    workingTimeForKilograms = workingTimeForKilograms.Where(w => w.Date >= filterWorkingTime.startDate && w.Date <= filterWorkingTime.endDate);

                    workingTimeForKilograms = workingTimeForKilograms.Where(w => w.Category == filterWorkingTime.category);

                    workingTimeForKilograms = workingTimeForKilograms.Include(w => w.Employee).OrderBy(w => w.Date);
                    var paginatedListting = new PaginatedListting<WorkingTimeForKilogram>();
                    paginatedListting.count = workingTimeForKilograms.Count();
                    paginatedListting.list = workingTimeForKilograms.ToList();
                    return paginatedListting;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar jornada laboral en base de datos: " + ex.InnerException);
            }
        }

        public async Task<bool> UpdateWorkingTime(WorkingTimeForKilogram workingTime)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    efc.workingTimeForKilograms.Update(workingTime);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar jornada laboral en base de datos: " + ex.Message);
            }
        }
    }
}
