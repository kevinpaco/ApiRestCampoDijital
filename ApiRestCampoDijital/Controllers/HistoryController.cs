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
    [Route("histories")]
    [Authorize]
    public class HistoryController:ControllerBase
    {
        private readonly IHistoryService historyService;

        public HistoryController(IHistoryService historyService)
        {
            this.historyService = historyService;
        }

        [HttpPost("agregar")]
        public IActionResult PostHistory([FromBody]HistoryDTO historyDTO) {

            var httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                this.historyService.AddHistory(historyDTO);
                httpVMResponses.Datos = true;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al guardar historial");
                httpVMResponses.Messages.Add($"{ex.InnerException}");
            }
            return Ok(httpVMResponses);
        }

        [HttpGet("listar")]
        public IActionResult GetListHistory(DateTime date)
        {

            var httpVMResponses = new HttpVMResponses<PaginatedListting<HistoryDTO>>();
            try
            {
                DateTime dateTime = DateTime.Now;
                date = date;
                httpVMResponses.Datos = this.historyService.ListHitories(date).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = null;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al listar historial");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpGet("buscar/{id}")]
        public IActionResult GetListHistory(int id = 0)
        {

            var httpVMResponses = new HttpVMResponses<HistoryDTO>();
            try
            {
                httpVMResponses.Datos = this.historyService.FindHistoryById(id).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = null;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al listar historial");
                httpVMResponses.Messages.Add($"{ex.InnerException}");
            }
            return Ok(httpVMResponses);
        }

    }
}
