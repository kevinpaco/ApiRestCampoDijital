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
    [Route("pagos")]
    [Authorize]
    public class PaymentController:ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        [HttpPost("agregar")]
        public IActionResult PostPayment([FromBody] PaymentDTO paymentDTO)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this._paymentService.AddPayment(paymentDTO).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al Guardar pago+ "+paymentDTO.EmployerId);
                httpVMResponses.Messages.Add(ex.Message);
            }
            return Ok(httpVMResponses);
        }

        [HttpGet("buscar/{id}")]
        public IActionResult GetPaymenById(int id)
        {
            HttpVMResponses<PaymentDTO> httpVMResponses = new HttpVMResponses<PaymentDTO>();
            try
            {
                httpVMResponses.Datos = this._paymentService.FindPaymentById(id).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = null;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al buscar pago");
                httpVMResponses.Messages.Add(ex.Message);
            }
            return Ok(httpVMResponses);
        }

        [HttpGet("listar")]
        public IActionResult GetAllFarmPayment(string textoBusqueda = null, int farmId = -1)
        {
            var httpVMResponses = new HttpVMResponses<PaginatedListting<PaymentDTO>>();
            try
            {
                httpVMResponses.Datos = this._paymentService.GetListPaymentOfFarm(textoBusqueda, farmId).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Datos = null;
                httpVMResponses.Messages.Add("Error al buscar pagos de finca");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }
    }
}
