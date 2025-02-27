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
    public interface IPaymentAdvanceService
    {
        Task<bool> AddPaymentAdvance(PaymentAdvanceDTO paymentAdvanceDto);
        Task<bool> ModifyPaymentAdvance(PaymentAdvanceDTO paymentAdvanceDto);
        Task<PaginatedListting<PaymentAdvanceDTO>> GetAllPaymentAdvance(int employeeDni, int farmId);
        Task<PaginatedListting<PaymentAdvanceDTO>> GetAllPaymentAdvanceOfFarm(int employeeDni, int farmId);
        Task<Decimal> GetTotalSalaryAdvanceOfEmployee(int employeeDni, int farmId);
    }
}
