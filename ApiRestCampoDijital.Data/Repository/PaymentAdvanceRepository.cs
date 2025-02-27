using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.Repository
{
    public class PaymentAdvanceRepository : IPaymentAdvanceRepository
    {
        public async Task<bool> AddPaymentAdvance(PaymentAdvance paymentAdvance)
        {
            try
            {
                using (var efc = new EfContext()) {
                 await efc.paymentAdvances.AddAsync(paymentAdvance);    
                 await efc.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar anticipo en base de datos: " + ex.Message);
            }
        }

        public async Task<bool> DeletePaymentAdvance(int paymentAdvanceId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ModifyPaymentAdvance(PaymentAdvance paymentAdvance)
        {
            try
            {
                using (var efc = new EfContext())
                {

                    efc.paymentAdvances.Update(paymentAdvance);
                    await efc.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar anticipo en base de datos: " + ex.Message);
            }
        }

        public async Task<PaginatedListting<PaymentAdvance>> GetAllPaymentAdvance(int employeDni,int farmId)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var query = efc.paymentAdvances.Where(p=> p.farmId==farmId && p.IsPaid==false);

                    if (employeDni != -1)
                    {
                        query = query.Where(p => p.EmployeeDNI.ToString().Contains(employeDni.ToString()));
                    }
                    
                    query = query.OrderBy(p => p.EmployeeDNI);                    
                    PaginatedListting<PaymentAdvance> paginatedListting = new PaginatedListting<PaymentAdvance>();
                    paginatedListting.count= query.Count();
                    paginatedListting.list = query.ToList();

                    return paginatedListting;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar anticipo en base de datos: " + ex.Message);
            }
        }

        public async Task<PaginatedListting<PaymentAdvance>> GetAllPaymentAdvanceOfFarm(int employeDni, int farmId)
        {
            try
            {
                using (var efc = new EfContext())
                {
                    var query = efc.paymentAdvances.Where(p => p.farmId == farmId);

                    if (employeDni != -1)
                    {
                        query = query.Where(p => p.EmployeeDNI.ToString().Contains(employeDni.ToString()));
                    }

                    query = query.OrderBy(p => p.EmployeeDNI);
                    PaginatedListting<PaymentAdvance> paginatedListting = new PaginatedListting<PaymentAdvance>();
                    paginatedListting.count = query.Count();
                    paginatedListting.list = query.ToList();

                    return paginatedListting;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar anticipo en base de datos: " + ex.Message);
            }
        }

    }
}
