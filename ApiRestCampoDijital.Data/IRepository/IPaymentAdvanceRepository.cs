using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.IRepository
{
    public interface IPaymentAdvanceRepository
    {
        Task<bool> AddPaymentAdvance(PaymentAdvance paymentAdvance);
        Task<bool> ModifyPaymentAdvance(PaymentAdvance paymentAdvance);
        Task<bool> DeletePaymentAdvance(int  paymentAdvanceId);
        Task<PaginatedListting<PaymentAdvance>> GetAllPaymentAdvance(int employeeDni,int farmId);
        Task<PaginatedListting<PaymentAdvance>> GetAllPaymentAdvanceOfFarm(int employeDni, int farmId);
    }
}
