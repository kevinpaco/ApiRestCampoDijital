using ApiRestCampoDijital.Layout;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using ApiRestCampoDijital.ServiceImplements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiRestCampoDijital.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("fincas")]
    [Authorize]
    public class FarmController: ControllerBase
    {
        private readonly IFarmService farmService;

        public FarmController(IFarmService farmService)
        {
            this.farmService = farmService;
        }

        [HttpGet(template: "lista")]
        public IActionResult GetListFarmsByIdEmployer(int idEmployer)
        {
            var httpVMResponses = new HttpVMResponses<PaginatedListting<FarmDTO>>();
            try
            {
                httpVMResponses.Datos = this.farmService.FindFarmByIdEmployer(idEmployer).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = null;
                httpVMResponses.Messages.Add("Error al listar fincas");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpPost(template: "nuevo")]
        public IActionResult PostEmployee([FromBody] FarmDTO farmDTO)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this.farmService.AddFarm(farmDTO).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al guardar finca: ");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpGet(template: "buscar")]
        public IActionResult FindEmployeeByIdFarm(int idFarm)
        {
            HttpVMResponses<FarmDTO> httpVMResponses = new HttpVMResponses<FarmDTO>();
            try
            {
                httpVMResponses.Datos = this.farmService.FindFarmById(idFarm).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = null;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al buscar finca: ");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpDelete(template: "eliminar/{id}")]
        public IActionResult DeleteFarm(int idFarm)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this.farmService.DeleteFarmByID(idFarm).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al eliminar finca: ");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }
    }
}
