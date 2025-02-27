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
    [Route("grupoDeTrabajo")]
    [Authorize]
    public class WorkingGroupController:ControllerBase
    {
        private readonly IWorkingGroupService workingGroupService;

        public WorkingGroupController(IWorkingGroupService workingGroupService)
        {
            this.workingGroupService = workingGroupService;
        }

        [HttpPost("agregar")]
        public IActionResult PostWorkingGroup([FromBody]List<int> DNIs,int farmId, int numberGroup)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
               // throw new Exception("sss: " + DNIs[0]+"/"+farmId+"/"+numberGroup);
                httpVMResponses.Datos = this.workingGroupService.UpdateEmployeeWitchWorkingGroup(DNIs,farmId, numberGroup).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al agregar grupo de trabajo");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpGet("listar")]
        public async Task<IActionResult> GetListWorkingGroup(int farmId, int numberGroup)
        {
            var httpVMResponses =new HttpVMResponses<PaginatedListting<EmployeeDTO>>(); 
            try
            {
                // throw new Exception("sss: " + DNIs[0]+"/"+farmId+"/"+numberGroup);
                httpVMResponses.Datos =await this.workingGroupService.getGroupEmployee(numberGroup,farmId);
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = null;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al buscar empleados de grupo de trabajo");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }


    }
}
