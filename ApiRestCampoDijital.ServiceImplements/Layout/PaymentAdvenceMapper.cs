using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    public class PaymentAdvenceMapper
    {
        public static PaymentAdvanceDTO GetPaymentAdvanceDTO(PaymentAdvance paymentAdvance)
        { 
            PaymentAdvanceDTO paymentAdvanceDTO = new PaymentAdvanceDTO()
            {
                Id = paymentAdvance.Id,
                AdvanceDate = paymentAdvance.AdvanceDate,
                Description = paymentAdvance.Description,
                EmployeeDni = paymentAdvance.EmployeeDNI,
                farmId = paymentAdvance.farmId,
                SalaryAdvance = paymentAdvance.SalaryAdvance,
                SupervisorName = paymentAdvance.SupervisorName,
                IsPaid = paymentAdvance.IsPaid,
     
            };

            return paymentAdvanceDTO;
        }

        public static List<PaymentAdvanceDTO> GetListPaymentAdvanceDTO(List<PaymentAdvance> paymentAdvances)
        {
            List<PaymentAdvanceDTO> paymentDTOs = new List<PaymentAdvanceDTO>();
            foreach (PaymentAdvance paymentAdvance in paymentAdvances)
            {
                PaymentAdvanceDTO paymentAdvanceDTO = GetPaymentAdvanceDTO(paymentAdvance);
                paymentDTOs.Add(paymentAdvanceDTO); 
            }
            return paymentDTOs;
        }

        public static PaymentAdvance GetPaymentAdvance(PaymentAdvanceDTO paymentAdvanceDTO)
        {
            PaymentAdvance paymentAdvance = new PaymentAdvance()
            {
                Id = paymentAdvanceDTO.Id,
                Description = paymentAdvanceDTO.Description,
                EmployeeDNI = paymentAdvanceDTO.EmployeeDni,
                farmId = paymentAdvanceDTO.farmId,
                SalaryAdvance = paymentAdvanceDTO.SalaryAdvance,
                SupervisorName = paymentAdvanceDTO.SupervisorName,
                IsPaid = paymentAdvanceDTO.IsPaid,
                AdvanceDate = paymentAdvanceDTO.AdvanceDate
            };
            return paymentAdvance;
        }

        public static List<PaymentAdvance> GetListPaymentAdvance(List<PaymentAdvanceDTO> paymentAdvancesDTO)
        {
            List<PaymentAdvance> payments = new List<PaymentAdvance>();
            foreach (PaymentAdvanceDTO paymentAdvanceDTO in paymentAdvancesDTO)
            {
                PaymentAdvance paymentAdvance =GetPaymentAdvance(paymentAdvanceDTO);
                payments.Add(paymentAdvance);
            }
            return payments;
        }
    }
}
