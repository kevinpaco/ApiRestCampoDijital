using ApiRestCampoDijital.Layout;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using ApiRestCampoDijital.ServiceImplements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using System.Net;

namespace ApiRestCampoDijital.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("empleado")]
    [Authorize]
    public class EmployeeController:ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet(template: "lista")]
        public IActionResult GetListEmployees(int idFarm, string textoBusqueda = null)
        {
            var httpVMResponses = new HttpVMResponses<PaginatedListting<EmployeeDTO>>();
            try
            {
                httpVMResponses.Datos= this.employeeService.ListEmployees(textoBusqueda , idFarm);
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = null;
                httpVMResponses.Messages.Add("Error al listar empleados");
                httpVMResponses.Messages.Add($"{ex.Message}");  
            }
            return Ok(httpVMResponses);
        }

        [HttpGet(template: "clasificadores")]
        public IActionResult GetListClasificadorEmployees(int idFarm)
        {
            var httpVMResponses = new HttpVMResponses<PaginatedListting<EmployeeDTO>>();
            try
            {
                httpVMResponses.Datos = this.employeeService.ListClasificadorEmployees(idFarm).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = null;
                httpVMResponses.Messages.Add("Error al listar empleados clasificadores");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpGet(template: "trabajadores_generales")]
        public IActionResult GetListHourEmployees(int idFarm)
        {
            var httpVMResponses = new HttpVMResponses<PaginatedListting<EmployeeDTO>>();
            try
            {
                httpVMResponses.Datos = this.employeeService.ListClasificadorEmployees(idFarm).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = null;
                httpVMResponses.Messages.Add("Error al listar empleados por hora");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpPost(template: "nuevo")]
        public IActionResult PostEmployee([FromBody]EmployeeDTO employeeDTO,int farmId) {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = employeeService.AddEmployee(farmId,employeeDTO).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al guardar empleado: ");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpPut(template:"actualizar")]
        public IActionResult PutEmployee([FromBody]EmployeeDTO employeeDTO)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = employeeService.EmployeeModify(employeeDTO).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al actualizar empleado");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpGet(template: "buscar")]
        public IActionResult GetEmployeByDni(int dni, int idFarm) {
            HttpVMResponses<EmployeeDTO> httpVMResponses = new HttpVMResponses<EmployeeDTO>();
            try
            {
                httpVMResponses.Datos = this.employeeService.FindEmployeeByDni(dni, idFarm).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = null;
                httpVMResponses.Messages.Add("Error al buscar empleado con dni: "+dni);
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpDelete(template: "eliminar")]
        public IActionResult DeleteEmployee(int idEmployee,int idFarm)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this.employeeService.DeleteEmployee(idEmployee,idFarm).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = false;
                httpVMResponses.Messages.Add("Error al eliminar empleado");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

    }
}
