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
    public class HistoryRepository : IHistoryRepository
    {
        public async void AddHistory(History history)
        {
            try
            {
                using (var efc = new EfContext()) {
                    await efc.histories.AddAsync(history);
                    await efc.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar historial: " + ex.InnerException);
            }
        }

        public async Task<History> FindHistoryById(int id)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    History history = await efc.histories.Where(h => h.Id == id).FirstOrDefaultAsync();
                    if (history != null)
                    {
                        return history;
                    }
                    else
                        throw new Exception("El historial no existe");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar historial: " + ex.InnerException);
            }
        }

        public Task<PaginatedListting<History>> ListHitories(DateTime date)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    PaginatedListting<History> paginatedListting = new PaginatedListting<History>();
                    var query = efc.histories.Where(h => h.Id != -1);
                    DateTime dateTime = DateTime.Now;
                    if(date!=dateTime)
                    {
                        query = query.Where(h => h.DateTime.Equals(date));
                    }

                    paginatedListting.count= query.Count();
                    paginatedListting.list = query.ToList();

                    return Task.FromResult(paginatedListting);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar historial de base da datos: " + ex.Message);
            }
        }
    }
}
