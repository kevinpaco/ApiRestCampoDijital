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
    [Route("anticipos")]
    [Authorize]
    public class PaymentAdvanceController: ControllerBase
    {
        private readonly IPaymentAdvanceService paymentAdvanceService;

        public PaymentAdvanceController(IPaymentAdvanceService paymentAdvanceService)
        {
            this.paymentAdvanceService = paymentAdvanceService;
        }

        [HttpPost("agregar")]
        public IActionResult PostPaymentAdvance([FromBody] PaymentAdvanceDTO paymentAdvanceDTO)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this.paymentAdvanceService.AddPaymentAdvance(paymentAdvanceDTO).Result;    
            }
            catch (Exception ex) {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                 httpVMResponses.Messages.Add("Error al Guardar anticipo");
                 httpVMResponses.Messages.Add(ex.Message);
            }
            return Ok(httpVMResponses);
        }

        [HttpPut("modificar")]
        public IActionResult PutPaymentAdvance([FromBody] PaymentAdvanceDTO paymentAdvanceDTO)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this.paymentAdvanceService.ModifyPaymentAdvance(paymentAdvanceDTO).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al modificar anticipo");
                httpVMResponses.Messages.Add(ex.Message);
            }
            return Ok(httpVMResponses);
        }

        [HttpGet("listarTodos")]
        public IActionResult GetAllPaymentAdvance(int employeeDni=-1, int farmId=-1)
        {
            var httpVMResponses = new HttpVMResponses<PaginatedListting<PaymentAdvanceDTO>>();
            try
            {
                if (farmId == -1)
                    throw new Exception("Finca no existe");
                httpVMResponses.Datos = this.paymentAdvanceService.GetAllPaymentAdvanceOfFarm(employeeDni, farmId).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = null;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al lista todos los anticipos");
                httpVMResponses.Messages.Add(ex.Message);
            }
            return Ok(httpVMResponses);
        }

        [HttpGet("totalSalaryAdvance")]
        public IActionResult GetSalaryAdvance(int employeeDni = -1, int farmId = -1)
        {
            var httpVMResponses = new HttpVMResponses<Decimal>();
            try
            {
                if (farmId == -1)
                    throw new Exception("Finca no existe");
                httpVMResponses.Datos = this.paymentAdvanceService.GetTotalSalaryAdvanceOfEmployee(employeeDni, farmId).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = -1;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al lista todos los anticipos");
                httpVMResponses.Messages.Add(ex.Message);
            }
            return Ok(httpVMResponses);
        }
    }
}
