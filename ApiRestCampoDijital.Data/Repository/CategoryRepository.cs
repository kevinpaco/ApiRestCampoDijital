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
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<bool> AddCategory(Category category)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var farm =await efc.farms.Where(f => !f.Deleted && f.Id == category.farmId).FirstOrDefaultAsync();
                    if (farm == null)
                        throw new Exception("Finca no existe");

                    farm.Categories.Add(category);
                    await efc.category.AddAsync(category);
                    await efc.SaveChangesAsync();
                    return true;    
                }
            }catch(Exception ex) 
                {
                   throw new Exception("Error al guardar categoria en base de datos: "+ex.Message);
                }
        }

        public async Task<PaginatedListting<Category>> GetAllCategories(int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    List<Category> listCtegory = await efc.category.Where(c => c.farmId==idFarm).ToListAsync();
                    PaginatedListting<Category> paginatedListting = new PaginatedListting<Category>();
                    paginatedListting.count= listCtegory.Count;
                    paginatedListting.list= listCtegory;
                    return paginatedListting;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar categorias de base da datos: " + ex.Message);
            }
        }

        public Task<bool> UpdateCategory(Category category)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    efc.category.Update(category);
                    efc.SaveChanges();
                   return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar categoria n base de datos: " + ex.Message);
            }
        }
    }
}
