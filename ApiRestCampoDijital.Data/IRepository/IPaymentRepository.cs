using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.IRepository
{
    public  interface IPaymentRepository
    {
        Task<int> AddPayment(Payment payment);

        Task<Payment> FindPaymentById(int id);
        Task<PaginatedListting<Payment>> GetListPaymentOfFarm(string textoBusqueda, int farmId);
    }
}
