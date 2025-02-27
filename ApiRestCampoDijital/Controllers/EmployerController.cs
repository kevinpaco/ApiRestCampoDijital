using ApiRestCampoDijital.Layout;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiRestCampoDijital.Controllers
{

    [EnableCors("MyPolicy")]
    [Route("employer")]
   
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService employerService;

        public EmployerController(IEmployerService employerService)
        {   
            this.employerService = employerService;
        }

        [HttpGet(template: "lista")]
        public IActionResult GetListEmployers(long cuit=0) {
            var httpVMResponses = new HttpVMResponses<PaginatedListting<EmployerDTO>>();
            try
            {
                httpVMResponses.Datos = employerService.ListEmployers(cuit);
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = null;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al listar empleadores");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpPost(template: "/nuevo")]
        public IActionResult PostEmployer([FromBody]EmployerDTO employerDTO)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                  httpVMResponses.Datos = employerService.AddEmployer(employerDTO).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al guardar empleador");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpGet(template: "buscar/{cuit}")]
        public IActionResult FindEmployerByCuit(long cuit=0)
        {
            HttpVMResponses<EmployerDTO> httpVMResponses = new HttpVMResponses<EmployerDTO>();
            try
            {
               httpVMResponses.Datos = employerService.FindEmployerByCuit(cuit).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = null;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al buscar empleador por cuit");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

      /*  [HttpDelete(template:"delete/{id}")]
        public IActionResult DeleteEmployer(int id =0) {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = employerService.DeleteEmployer(id).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al eliminar empleador");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }*/
    }
}
