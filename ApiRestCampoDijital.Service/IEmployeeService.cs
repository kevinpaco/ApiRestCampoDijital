using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRestCampoDijital.ModelDTO;

namespace ApiRestCampoDijital.Service
{
    public interface IEmployeeService
    {
        Task<bool> AddEmployee(int farmID,EmployeeDTO employeeDto);
        Task<EmployeeDTO> EmployeeAutentication(long cuit, string password);
        Task<bool> EmployeeModify(EmployeeDTO employeeDto);
        Task<EmployeeDTO> FindEmployeeById(int employeeId);
        Task<EmployeeDTO> FindEmployeeByDni(int dni, int idFarm);
        Task<bool> DeleteEmployee(int dniEmployee,int idFarm);
        PaginatedListting<EmployeeDTO> ListEmployees(string textoBusqueda, int idFarm);
        Task<PaginatedListting<EmployeeDTO>> ListClasificadorEmployees(int idFarm);
        Task<PaginatedListting<EmployeeDTO>> ListHourEmployees(int idFarm);
    }
}
