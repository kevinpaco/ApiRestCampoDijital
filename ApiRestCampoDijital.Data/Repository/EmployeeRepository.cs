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
    public class EmployeeRepository : IEmployeeRepository
    {
        public async Task<bool> AddEmployee(int farmId,Employee employee)
        {
            try
            {
                using (var efc = new EfContext())
                {  
                    //obtenemos categorias de base da datos
                    var categories = await efc.category.Where(c => employee.Categories.Contains(c)).ToListAsync();
                    //asignamos a empleados
                    employee.Categories = categories;
                    //guardamos
                    await efc.employee.AddAsync(employee);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar employee en base de datos: " + ex.InnerException);
            }
        }

        public async Task<bool> AddCategoriesToEmployeeExistingInDataBase(Employee employee)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    //obtenemos categorias de base da datos
                    var categories = await efc.category.Where(c => employee.Categories.Contains(c)).ToListAsync();
                    //limpiar categorias de empleado
                    employee.Categories.Clear();
                    //asignamos las nuevas categorias a empleado                    
                    employee.Categories = categories;
                    //guardamos
                    efc.employee.Update(employee);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar nuevas categorias a employee en base de datos: " + ex.InnerException);
            }
        }

        public async Task<bool> DeleteEmployee(int idEmployee, int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    // Obtener el empleado existente junto con sus categorías
                    var employee = await efc.employee
                        .Include(e => e.Categories)
                        .FirstOrDefaultAsync(e => e.Id == idEmployee);

                    if (employee == null)
                    {
                        throw new Exception("El empleado no existe.");
                    }

                    // Obtener las categorías de la finca
                    var categoriasOfFarm = await efc.category
                        .Where(c => c.farmId == idFarm)
                        .ToListAsync();

                    // Obtener las categorías del empleado que pertenecen a la finca
                    var categoriesOfEmployee = categoriasOfFarm
                        .Where(c => employee.Categories.Any(ce => ce.Id == c.Id))
                        .ToList();

                    if (categoriesOfEmployee.Count!=0)
                    {
                        // Desvincular las categorías del empleado
                        foreach (var category in categoriesOfEmployee)
                        {
                            employee.Categories.Remove(category);
                        }
                        // Actualizar el empleado
                        efc.employee.Update(employee);
                        await efc.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Error: el Empleado a eliminar no existe en las categorías de la finca.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar employee en base de datos: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        public async Task<bool> EmployeeModify(Employee employee, int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    // Obtener el empleado existente junto con sus categorías
                    var currentEmployee = await efc.employee
                        .Include(e => e.Categories)
                        .FirstOrDefaultAsync(e => e.Id == employee.Id);

                    if (currentEmployee == null)
                    {
                        throw new Exception("El empleado no existe.");
                    }

                    // Obtener las categorías de la finca
                    var categoriasOfFarm = await efc.category
                        .Where(c => c.farmId == idFarm)
                        .ToListAsync();

                    // Obtener las categorías del empleado que pertenecen a la finca
                    var categoriesOfEmployee = categoriasOfFarm
                        .Where(c => employee.Categories.Any(ce => ce.Id == c.Id))
                        .ToList();

                    // Eliminar las relaciones actuales de las categorías del empleado
                    currentEmployee.Categories.Clear();

                    // Asignar nuevas categorías desde la base de datos
                    foreach (var category in categoriesOfEmployee)
                    {
                        currentEmployee.Categories.Add(category);
                    }

                    // Actualizar las propiedades del empleado (ya vienen actualizadas)
                    currentEmployee.Name = employee.Name;
                    currentEmployee.Surname = employee.Surname;
                    currentEmployee.Dni = employee.Dni;
                    currentEmployee.IsSupervisor = employee.IsSupervisor;
                    currentEmployee.WorkingGroupId = employee.WorkingGroupId;

                    // Guardar los cambios
                    efc.employee.Update(currentEmployee);
                    await efc.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar employee en base de datos: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }


        public async Task<Employee> FindEmployeeByDni(int dni,int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var categories = efc.category.Where(c => c.farmId == idFarm);                   

                    Employee? employer = await efc.employee.Where(e => e.Dni == dni && categories.Any(c=> c.Employees.Contains(e))).Include(e => e.Categories).FirstOrDefaultAsync();
                    if (employer != null)
                    {
                        return employer;
                    }
                    else
                    {
                        throw new Exception("El empleado no existe en la finca");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar empleado en una finca: " + ex.Message);
            }
        }

        public async Task<bool> IsEmployeeExistingInFarm(int dni, int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var categories = efc.category.Where(c => c.farmId == idFarm);

                    Employee? employer = await efc.employee.Where(e => e.Dni == dni && categories.Any(c => c.Employees.Contains(e))).FirstOrDefaultAsync();
                    if (employer != null)
                        return true;                    
                    else
                        return false;
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar si empleado existe en una finca: " + ex.Message);
            }
        }

        public async Task<Employee> IsEmployeeExistingInDataBase(int dni)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    Employee? employer = await efc.employee.Where(e => e.Dni == dni).FirstOrDefaultAsync();
                    if (employer != null)
                    {
                        return employer;
                    }
                    else
                    {
                        return null;//throw new Exception("El empleado no existe en base de datos: " + dni);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar empleado en base de datos: " + ex.Message);
            }
        }

        public async Task<Employee> FindEmployeeById(int employeeId)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    Employee? employer = await efc.employee.Where(e => !e.Deleted && e.Id == employeeId).FirstOrDefaultAsync();
                    if (employer != null)
                    {
                        return employer;
                    }
                    else
                    {
                        throw new Exception("El empleado no existe en base de datos: "+ employeeId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar empleado en base de datos: " + ex.Message);
            }
        }

        public Task<PaginatedListting<Employee>> ListEmployees(string textoBusqueda,int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    PaginatedListting<Employee> paginatedListting = new PaginatedListting<Employee>();
                    
                    var categories = efc.category.Where(c => c.farmId ==idFarm);

                    var employeesOfFarm = efc.employee.Where(e => categories.Any(c => c.Employees.Contains(e)));

                    var query = efc.employee.Where(e => !e.Deleted);
                 //   Console.WriteLine("busqueda en base: "+query.Count());
                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        //buscamos por nombre y apellido
                        employeesOfFarm = employeesOfFarm.Where(e => e.Name.Contains(textoBusqueda) || e.Surname.Contains(textoBusqueda));
                    }
                    //incluimos categorias
                    employeesOfFarm = employeesOfFarm.Include(e => e.Categories.Where(c => c.farmId == idFarm));
                    paginatedListting.count = employeesOfFarm.Count();
                   //    Console.WriteLine("sale de busqueda: "+paginatedListting.count);
                    paginatedListting.list = employeesOfFarm.OrderBy(e => e.Id).ToList();
                    return Task.FromResult(paginatedListting);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar empleados de base de datos: " + ex.Message);
            }
        }

        public Task<PaginatedListting<Employee>> ListClasificadorEmployees(int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    PaginatedListting<Employee> paginatedListting = new PaginatedListting<Employee>();

                    var categories = efc.category.Where(c => c.farmId == idFarm && c.Name=="Clasificador");

                    var employeesOfFarm = efc.employee.Where(e => categories.Any(c => c.Employees.Contains(e)));

                    var query = efc.employee.Where(e => !e.Deleted);
                    //incluimos categorias
                    employeesOfFarm = employeesOfFarm.Include(e => e.Categories.Where(c => c.farmId == idFarm));
                    paginatedListting.count = employeesOfFarm.Count();
                    paginatedListting.list = employeesOfFarm.OrderBy(e => e.Id).ToList();
                    return Task.FromResult(paginatedListting);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar empleados de base de datos: " + ex.Message);
            }
        }

        public Task<PaginatedListting<Employee>> ListHourEmployees(int idFarm)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    PaginatedListting<Employee> paginatedListting = new PaginatedListting<Employee>();

                    var categories = efc.category.Where(c => c.farmId == idFarm && c.Name == "Peon General");

                    var employeesOfFarm = efc.employee.Where(e => categories.Any(c => c.Employees.Contains(e)));

                    var query = efc.employee.Where(e => !e.Deleted);
                    //incluimos categorias
                    employeesOfFarm = employeesOfFarm.Include(e => e.Categories.Where(c => c.farmId == idFarm));
                    paginatedListting.count = employeesOfFarm.Count();
                    paginatedListting.list = employeesOfFarm.OrderBy(e => e.Id).ToList();
                    return Task.FromResult(paginatedListting);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar empleados de base de datos: " + ex.Message);
            }
        }
    }
}
