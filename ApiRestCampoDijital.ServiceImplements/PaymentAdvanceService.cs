using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Data.Repository;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using ApiRestCampoDijital.ServiceImplements.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements
{
    public class PaymentAdvanceService : IPaymentAdvanceService
    {
        private readonly IPaymentAdvanceRepository paymentAdvanceRepository;
        private readonly IEmployeeService employeeService;
        public PaymentAdvanceService(IPaymentAdvanceRepository paymentAdvanceRepository, IEmployeeService employeeService)
        {
            this.paymentAdvanceRepository = paymentAdvanceRepository;
            this.employeeService = employeeService;
        }

        public Task<bool> AddPaymentAdvance(PaymentAdvanceDTO paymentAdvanceDto)
        {
            PaymentAdvance paymentAdvance = PaymentAdvenceMapper.GetPaymentAdvance(paymentAdvanceDto);

            return paymentAdvanceRepository.AddPaymentAdvance(paymentAdvance);
        }

        public async Task<PaginatedListting<PaymentAdvanceDTO>> GetAllPaymentAdvance(int employeeDni,int farmId)
        {
            PaginatedListting<PaymentAdvance> paginatedListting =await this.paymentAdvanceRepository.GetAllPaymentAdvance(employeeDni,farmId);

            PaginatedListting<PaymentAdvanceDTO> paginatedListtingDTO = new PaginatedListting<PaymentAdvanceDTO>();
            paginatedListtingDTO.count = paginatedListting.count;
            foreach (var payAd in paginatedListting.list)
            {
                var payDto = PaymentAdvenceMapper.GetPaymentAdvanceDTO(payAd);
                payDto.Employee = await this.employeeService.FindEmployeeByDni(payAd.EmployeeDNI, payAd.farmId);
                paginatedListtingDTO.list.Add(payDto);
            }

            return paginatedListtingDTO;
        }

        public async Task<PaginatedListting<PaymentAdvanceDTO>> GetAllPaymentAdvanceOfFarm(int employeeDni, int farmId)
        {
            PaginatedListting<PaymentAdvance> paginatedListting = await this.paymentAdvanceRepository.GetAllPaymentAdvanceOfFarm(employeeDni,farmId);

            PaginatedListting<PaymentAdvanceDTO> paginatedListtingDTO = new PaginatedListting<PaymentAdvanceDTO>();
            paginatedListtingDTO.count = paginatedListting.count;
            foreach (var payAd in paginatedListting.list)
            { 
                var payDto = PaymentAdvenceMapper.GetPaymentAdvanceDTO(payAd);
                payDto.Employee = await this.employeeService.FindEmployeeByDni(payAd.EmployeeDNI, payAd.farmId);
                paginatedListtingDTO.list.Add(payDto);
            }

            return paginatedListtingDTO;
        }

        public async Task<Decimal> GetTotalSalaryAdvanceOfEmployee(int employeeDni, int farmId)
        {
            PaginatedListting<PaymentAdvance> paginatedListting = await this.paymentAdvanceRepository.GetAllPaymentAdvance(employeeDni, farmId);
            Decimal totalSalaryAdvance = 0;
            foreach (var payAd in paginatedListting.list)
            {
                totalSalaryAdvance += payAd.SalaryAdvance;
            }

            return totalSalaryAdvance;
        }

        public async Task<bool> ModifyPaymentAdvance(PaymentAdvanceDTO paymentAdvanceDto)
        {
            PaymentAdvance paymentAdvance = PaymentAdvenceMapper.GetPaymentAdvance(paymentAdvanceDto);
            return await paymentAdvanceRepository.ModifyPaymentAdvance(paymentAdvance);
        }
    }
}
