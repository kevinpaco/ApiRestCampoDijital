using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model.workingTime;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.Repository
{
    public class WorkingTimeInGroupRepository : IWorkingTimeInGroupRepository
    {
        public async Task<int> AddWorkingTimeInGroup(WorkingTimeInGroup workingTimeInGroup)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    await efc.workingTimeGroups.AddAsync(workingTimeInGroup);
                    await efc.SaveChangesAsync();
                    return workingTimeInGroup.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar un grupo de trabajo: "+ex.InnerException);
            }
        }

        public async Task<int> UpdateWorkingTimeInGroup(WorkingTimeInGroup workingTimeInGroup)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    efc.workingTimeGroups.Update(workingTimeInGroup);
                    await efc.SaveChangesAsync();
                    return workingTimeInGroup.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar un grupo de trabajo: " + ex.InnerException);
            }
        }

        public async Task<WorkingTimeInGroup> FindWorkingTimeInGroupById(int idWorking, int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    WorkingTimeInGroup workingTimeInGroup = await efc.workingTimeGroups.FirstOrDefaultAsync(w=>w.Id==idWorking && w.farmId==idFarm);
                    if (workingTimeInGroup == null)
                        throw new Exception("Error al buscar, trabajo en grupo no existe");
                    return workingTimeInGroup;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar un grupo de trabajo: " + ex.InnerException);
            }
        }

        public async Task<PaginatedListting<WorkingTimeInGroup>> GetAllWorkingTimeInGroup(DateTime dateTime, int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var workingTimesInGroup = efc.workingTimeGroups.Where(w => w.farmId == idFarm);
                    if (!dateTime.Equals(new DateTime(2000,01,01)))
                    {
                        workingTimesInGroup = workingTimesInGroup.Where(w=>w.Date.Equals(dateTime));
                    }
                    workingTimesInGroup = workingTimesInGroup.OrderBy(w => w.Id);
                    var paginatedListting=new PaginatedListting<WorkingTimeInGroup>();
                    paginatedListting.count = workingTimesInGroup.Count();
                    paginatedListting.list = workingTimesInGroup.ToList();
                   // throw new Exception("numero de workingTimes: " + workingTimesInGroup.ToArray()[0].WorkingTimes.Count);
                    return paginatedListting;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar grupo de trabajo: " + ex.Message);
            }
        }
    }
}
