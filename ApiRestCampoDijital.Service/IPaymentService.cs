using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Service
{
    public interface IPaymentService
    {
        Task<bool> AddPayment(PaymentDTO paymentDTO);

        Task<PaymentDTO> FindPaymentById(int id);
        Task<PaginatedListting<PaymentDTO>> GetListPaymentOfFarm(string textoBusqueda, int farmId);
    }
}
