using ApiRestCampoDijital.Layout;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;

namespace ApiRestCampoDijital.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("JornadasPorHora")]
    [Authorize]
    public class WorkingTimeForHourController : ControllerBase
    {
        private readonly IWorkingTimeForHourService _workingTimeForHourService;
        public WorkingTimeForHourController(IWorkingTimeForHourService workingTimeForHourService)
        {
            this._workingTimeForHourService = workingTimeForHourService;
        }

        [HttpPost("agregar")]
        public IActionResult PostWorkginTimeForHour([FromBody] WorkingTimeDTO workingTimeDTO)
        {
            var httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this._workingTimeForHourService.AddWorkingTime(workingTimeDTO).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = false;
                httpVMResponses.Messages.Add("Error al agregar nueva Jornada laboral");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpPut("actualizar")]
        public IActionResult PutWorkginTimeForHour([FromBody] WorkingTimeDTO workingTimeDTO)
        {
            var httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this._workingTimeForHourService.UpdateWorkingTime(workingTimeDTO).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = false;
                httpVMResponses.Messages.Add("Error al actualizar Jornada laboral");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpGet("lista")]
        public IActionResult GetAllWorkginTimeForHour(DateTime? dateTime, int idFarm = -1)
        {
            var httpVMResponses = new HttpVMResponses<PaginatedListting<WorkingTimeDTO>>();
            try
            {
                var FindDateTime = dateTime ?? new DateTime(2000, 01, 01);
                if (idFarm == -1)
                    throw new Exception("Error el id de finca no existe");
                httpVMResponses.Datos = this._workingTimeForHourService.ListWorkingTime(FindDateTime, idFarm).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = null;
                httpVMResponses.Messages.Add("Error al listar Jornadas laborales");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpGet("listaNoPagados")]
        public IActionResult GetAllWorkginTimeForHourNotPaid([FromQuery] string filterWorkingTimeQuery, int idFarm = -1)
        {
            var httpVMResponses = new HttpVMResponses<PaginatedListting<WorkingTimeDTO>>();
            try
            {
                if (idFarm == -1)
                    throw new Exception("Error el id de finca no existe");
               
                var filterWorkingTime = JsonConvert.DeserializeObject<FilterWorkingTimeDTO>(filterWorkingTimeQuery);
                    var filterWorkingTimeDTO = new FilterWorkingTimeDTO()
                    {
                        startDate= filterWorkingTime.startDate,
                        endDate = filterWorkingTime.endDate,
                        category = filterWorkingTime.category,
                        EmployerDNI = filterWorkingTime.EmployerDNI
                    };

               // throw new Exception("value:: " + filterWorkingTimeDTO.ToString() + "/" + idFarm);
                httpVMResponses.Datos = this._workingTimeForHourService.ListWorkingTimeNotPaid(filterWorkingTimeDTO,idFarm).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = null;
                httpVMResponses.Messages.Add("Error al listar Jornadas laborales");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpGet("buscar/{idWorking}")]
        public IActionResult FindWorkginTimeForHour(int idWorking)
        {
            var httpVMResponses = new HttpVMResponses<WorkingTimeDTO>();
            try
            {
                httpVMResponses.Datos = this._workingTimeForHourService.FindWorkingTimeById(idWorking).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = null;
                httpVMResponses.Messages.Add("Error al buscar Jornada laboral");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }
    }
}
