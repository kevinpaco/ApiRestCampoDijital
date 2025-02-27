using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.workingTime;
using ApiRestCampoDijital.ModelDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    public class PaymentMapper
    {
        public static Payment GetPayment(PaymentDTO paymentDTO)
        {
            Payment payment = new Payment()
            {
                StartDate = paymentDTO.StartDate,
                TotalSalary = paymentDTO.TotalSalary,
                PaymentDate = paymentDTO.PaymentDate,
                EmployeeDni = paymentDTO.EmployeeDni,
                EmployerName = paymentDTO.EmployerName,
                FarmId = paymentDTO.FarmId, 
                HoursNumber = paymentDTO.HoursNumber,
                KilogramNumber = paymentDTO.KilogramNumber,
                Category = paymentDTO.Category,
                EmployerId = paymentDTO.EmployerId,
            };
            return payment;
        }

        public static List<PaymentDTO> GetListPaymentDTO(List<Payment> payments)
        {
            List<PaymentDTO> paymentList = new List<PaymentDTO>();
            foreach (Payment payment in payments)
            {
                PaymentDTO paymentDTO = GetPaymentDTO(payment);
               
                paymentList.Add(paymentDTO);
            }
            
            return paymentList;
        }

        public static PaymentDTO GetPaymentDTO(Payment payment)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Payment, PaymentDTO>()
                                                        .ForMember(dest => dest.WorkingTimes, opt => opt.Ignore())
                                                        .ForMember(dest => dest.Employee, opt => opt.Ignore()));
           
            var mapper = config.CreateMapper();
            PaymentDTO paymentDTO = mapper.Map<PaymentDTO>(payment);
            
            foreach(WorkingTime workingTime in payment.WorkingTimes)
            {
                paymentDTO.WorkingTimes.Add(GetWorkingTime(workingTime));
            }

            return paymentDTO;
        }

        private static WorkingTimeDTO GetWorkingTime(WorkingTime workingTime)
        {
            WorkingTimeDTO workingTimeDTO = new WorkingTimeDTO()
            {
                Id = workingTime.Id,
                AttendantName = workingTime.AttendantName,
                Category = workingTime.Category,
                Date = workingTime.Date,
                EmployeeId = workingTime.EmployeeId,
                Paid = workingTime.Paid,
                Present = workingTime.Present,
                FarmId = workingTime.FarmId

            };
            return workingTimeDTO;
        }

    }
}
