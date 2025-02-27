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
    [Route("gruposDeTrabajo")]
    [Authorize]
    public class WorkingTimeInGroupController: ControllerBase
    {
        private readonly IWorkingTimeInGroupService _workingTimeInGroupService;

        public WorkingTimeInGroupController(IWorkingTimeInGroupService workingTimeInGroupService)
        {
            this._workingTimeInGroupService = workingTimeInGroupService;
        }

        [HttpGet(template: "lista")]
        public IActionResult GetListEmployees(int idFarm, DateTime? dateTime)
        {
            var httpVMResponses = new HttpVMResponses<PaginatedListting<WorkingTimeInGroupDTO>>();
            try
            {
                var dateTimeFilter = dateTime ?? new DateTime(2000,01,01);

                httpVMResponses.Datos = this._workingTimeInGroupService.GetAllWorkingTimeInGroup(dateTimeFilter,idFarm).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = null;
                httpVMResponses.Messages.Add("Error al listar grupos de trabajo");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpPost(template: "nuevo")]
        public IActionResult PostEmployee([FromBody]WorkingTimeInGroupDTO workingTimeInGroupDTO)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this._workingTimeInGroupService.AddWorkingTimeInGroup(workingTimeInGroupDTO).Result;
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

        [HttpPut(template: "actualizar")]
        public IActionResult PutEmployee([FromBody] WorkingTimeInGroupDTO workingTimeInGroupDTO)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this._workingTimeInGroupService.UpdateWorkingTimeInGroup(workingTimeInGroupDTO).Result;
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


        [HttpGet(template: "buscar")]
    public IActionResult GetEmployee(int id,int idFarm)
    {
        var httpVMResponses = new HttpVMResponses<WorkingTimeInGroupDTO>();
        try
        {
                httpVMResponses.Datos = this._workingTimeInGroupService.FindWorkingTimeInGroupById(id, idFarm).Result;
        }
        catch (Exception ex)
        {
            httpVMResponses.Datos = null;
            httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
            httpVMResponses.Messages.Add("Error al guardar empleado: ");
            httpVMResponses.Messages.Add($"{ex.Message}");
        }
        return Ok(httpVMResponses);
        } 
  }
    
}



