using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model.workingTime;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using ApiRestCampoDijital.ServiceImplements.Layout;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IWorkingTimeForHourService workingTimeForHourService;
        private readonly IWorkingTimeForKilogramService workingTimeForKilogramService;
        private readonly IPaymentAdvanceService paymentAdvanceService;
        private readonly IHistoryService historyService;
        private readonly IEmployeeService employeeService;
        public PaymentService(IPaymentRepository paymentRepository, 
                              IWorkingTimeForHourService workingTimeForHourService,
                              IPaymentAdvanceService paymentAdvanceService,
                              IHistoryService historyService,
                              IEmployeeService employeeService,
                              IWorkingTimeForKilogramService workingTimeForKilogramService)
        {
            this.paymentRepository = paymentRepository;
            this.workingTimeForHourService = workingTimeForHourService;
            this.paymentAdvanceService = paymentAdvanceService;
            this.historyService = historyService;
            this.employeeService = employeeService;
            this.workingTimeForKilogramService = workingTimeForKilogramService;
        }
        public async Task<bool> AddPayment(PaymentDTO paymentDTO)
        {
            Payment payment = PaymentMapper.GetPayment(paymentDTO);
            PaginatedListting<PaymentAdvanceDTO> paginatedPaymentAdvance =await this.paymentAdvanceService.GetAllPaymentAdvance(paymentDTO.EmployeeDni, paymentDTO.FarmId);
          
            int paymentId = await this.paymentRepository.AddPayment(payment);
            addHistory($"{paymentDTO.EmployerName} agrego un nuevo pago al empleado con dni {paymentDTO.EmployeeDni}");
            _ = await ModifyPaymentAdvance(paginatedPaymentAdvance.list);

            return await ModifyWorkingTimeToPaid(paymentDTO.WorkingTimes,paymentId);
        }

        private async Task<bool> ModifyWorkingTimeToPaid(List<WorkingTimeDTO> workingTimeDTOs,int paymentId)
        {
            foreach(var workingTime in workingTimeDTOs)
            { 
                workingTime.Paid = true;
                workingTime.PaymentId= paymentId;
                _= await this.workingTimeForHourService.UpdateWorkingTime(workingTime);
            }

            return true;
        }

        private async Task<bool> ModifyPaymentAdvance(List<PaymentAdvanceDTO> paymentAdvanceDTOs)
        {
            foreach (var paymentAdvance in paymentAdvanceDTOs)
            {
                paymentAdvance.IsPaid = true;
                await this.paymentAdvanceService.ModifyPaymentAdvance(paymentAdvance);
            }
            return true;
        }

        public Task<PaymentDTO> FindPaymentById(int id)
        {
            Payment payment= this.paymentRepository.FindPaymentById(id).Result;
            PaymentDTO paymentDTO = PaymentMapper.GetPaymentDTO(payment);
            return Task.FromResult(paymentDTO);
        }

        public async Task<PaginatedListting<PaymentDTO>> GetListPaymentOfFarm(string textoBusqueda, int farmId)
        {
            if (farmId != -1)
            {
                PaginatedListting<Payment> paginatedListting =await this.paymentRepository.GetListPaymentOfFarm(textoBusqueda, farmId);
                PaginatedListting<PaymentDTO> paginatedListtingDTO = new PaginatedListting<PaymentDTO>();
                paginatedListtingDTO.count = paginatedListting.count;
                paginatedListtingDTO.list = PaymentMapper.GetListPaymentDTO(paginatedListting.list);
                
                //asignar employee a payment
               await AddEmployeeToPayment(paginatedListtingDTO.list);

                return paginatedListtingDTO;
            }
            else
            {
                throw new Exception("Error el ID de employer no valido !!!");
            }
        }

        private async Task AddEmployeeToPayment(List<PaymentDTO> paymentDTOs)
        {
            foreach (PaymentDTO paymentDTO in paymentDTOs)
            {
                var employeeDTO =await this.employeeService.FindEmployeeByDni(paymentDTO.EmployeeDni,paymentDTO.FarmId);
                paymentDTO.EmployeeName = employeeDTO.Name + " " + employeeDTO.Surname;
            }
        }

        private void addHistory(string description)
        {
            HistoryDTO historyDTO = new HistoryDTO()
            {
                Description = description,
            };
            this.historyService.AddHistory(historyDTO);
        }
    }
}
