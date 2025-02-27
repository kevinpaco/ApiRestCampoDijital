using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public async Task<int> AddPayment(Payment payment)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    
                    await efc.payments.AddAsync(payment);
                    await efc.SaveChangesAsync();
                    return payment.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar pago en base de datos: "+"id: "+ payment.EmployerId+" error: " + ex.InnerException);
            }
        }

        public async Task<Payment> FindPaymentById(int id)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    Payment? payment = await efc.payments.Where(p => p.Id == id).FirstOrDefaultAsync();
                    if (payment != null)
                    {
                        return payment;
                    }
                    else
                    {
                        throw new Exception("El pago no existe en base de datos");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar pago en base de datos: " + ex.Message);
            }
        }

        public Task<PaginatedListting<Payment>> GetListPaymentOfFarm(string textoBusqueda, int farmId)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var query = efc.payments.Where(p => p.FarmId == farmId);
                    if (!string.IsNullOrEmpty(textoBusqueda))
                    {
                        query = query.Where(p => p.EmployeeName.Contains(textoBusqueda));
                    }

                    PaginatedListting<Payment> paginatedListting = new PaginatedListting<Payment>();
                    paginatedListting.count = query.Count();
                    paginatedListting.list = query.OrderBy(p => p.EmployeeName).ToList();
                    return Task.FromResult(paginatedListting);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar pagos de una finca en base de datos: " + ex.InnerException);
            }
        }

    }
}
