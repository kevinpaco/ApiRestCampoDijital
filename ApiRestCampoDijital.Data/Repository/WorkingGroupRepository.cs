using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.Repository
{
    public class WorkingGroupRepository : IWorkingGroupRepository
    {

        public async Task<PaginatedListting<Employee>> getGroupEmployee(int numberGroup,int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    // Buscar las categorías de una finca
                    var farmCategories = await efc.category.Where(c => c.farmId == idFarm).ToListAsync();

                    // Buscar los empleados de esas categorías
                    var employees = await efc.employee
                        .Where(e => e.Categories.Any(c => farmCategories.Contains(c)) && e.WorkingGroupId == numberGroup)
                        .OrderBy(e => e.Name)
                        .ToListAsync();

                    // Colocar los resultados en la lista paginada
                    PaginatedListting<Employee> paginatedListing = new PaginatedListting<Employee>
                    {
                        count = employees.Count,
                        list = employees
                    };

                    return paginatedListing;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar empleados de grupo de trabajo en base de datos: " + ex.Message);
            }
        }

    }
}
