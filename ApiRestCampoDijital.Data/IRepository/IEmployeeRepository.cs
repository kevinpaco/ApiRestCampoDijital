using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.IRepository
{
    public interface IEmployeeRepository
    {
        Task<bool> AddEmployee(int famrId,Employee employee);
        Task<bool> AddCategoriesToEmployeeExistingInDataBase(Employee employee);
        Task<bool> IsEmployeeExistingInFarm(int dni, int idFarm);
        Task<bool> EmployeeModify(Employee employee,int idFarm);
        Task<Employee> FindEmployeeById(int employeeId);
        Task<Employee> FindEmployeeByDni(int dni,int idFarm);
        Task<Employee> IsEmployeeExistingInDataBase(int dni);
        Task<bool> DeleteEmployee(int idEmployee,int idFarm);
        Task<PaginatedListting<Employee>> ListEmployees(string textoBusqueda,int idFarm);
        Task<PaginatedListting<Employee>> ListClasificadorEmployees(int idFarm);
        Task<PaginatedListting<Employee>> ListHourEmployees(int idFarm);
    }
}
