using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model;
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
    public class WorkingTimeForHourRepository : IWorkingTimeForHourRepository
    {
        public async Task<bool> AddWorkingTime(WorkingTimeForHour workingTime)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    await efc.workingTimeForHours.AddAsync(workingTime);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar jornada laboral en base de datos: " + ex.InnerException);
            }
        }

        public async Task<WorkingTimeForHour> FindWorkingTimeById(int idWorkingTime)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var workingTime = await efc.workingTimeForHours.FirstOrDefaultAsync(w=>w.Id==idWorkingTime);
                    if (workingTime != null)
                        return workingTime;
                    else
                        throw new Exception("Erro al buscar jornada laboral en base de datos: ");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar jornada laboral en base de datos: " + ex.InnerException);
            }
        }

        public async Task<PaginatedListting<WorkingTimeForHour>> ListWorkingTime(DateTime dateTime, int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var workingTimeForHours = efc.Set<WorkingTimeForHour>().Where(w => w.FarmId == idFarm);
                  //  throw new Exception("ess: " + dateTime + "/" + idFarm);
                    if(!dateTime.Equals(new DateTime(2000,01,01)))
                    {
                        workingTimeForHours = workingTimeForHours.Where(w=> w.Date.Equals(dateTime));
                    }
                    workingTimeForHours = workingTimeForHours.Include(w=> w.Employee);
                    PaginatedListting<WorkingTimeForHour> paginatedListting =new PaginatedListting<WorkingTimeForHour>();
                    paginatedListting.count = workingTimeForHours.Count();
                    paginatedListting.list = workingTimeForHours.ToList();                  
                    return paginatedListting;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar jornada laboral en base de datos: " + ex.InnerException);
            }
        }

        public async Task<PaginatedListting<WorkingTimeForHour>> ListWorkingTimeNotPaid(FilterWorkingTime filterWorkingTime,int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var workingTimeForHours = efc.Set<WorkingTimeForHour>().Where(w => w.FarmId == idFarm && w.Paid == false && w.EmployeeId==filterWorkingTime.EmployeeId);
                    //  throw new Exception("ess: " + dateTime + "/" + idFarm);

                    workingTimeForHours = workingTimeForHours.Where(w=> w.Date >= filterWorkingTime.startDate && w.Date <= filterWorkingTime.endDate);

                    workingTimeForHours = workingTimeForHours.Where(w => w.Category == filterWorkingTime.category);

                    workingTimeForHours = workingTimeForHours.Include(w => w.Employee).OrderBy(w => w.Date);
                    PaginatedListting<WorkingTimeForHour> paginatedListting = new PaginatedListting<WorkingTimeForHour>();
                    paginatedListting.count = workingTimeForHours.Count();
                    paginatedListting.list = workingTimeForHours.ToList();
                    return paginatedListting;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar jornada laboral en base de datos: " + ex.InnerException);
            }
        }

        public async Task<bool> UpdateWorkingTime(WorkingTimeForHour workingTime)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    efc.workingTimeForHours.Update(workingTime);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar jornada laboral en base de datos: " + ex.InnerException);
            }
        }
    }
}
