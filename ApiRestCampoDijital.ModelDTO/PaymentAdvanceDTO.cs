using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ModelDTO
{
    public class PaymentAdvanceDTO
    {
        public int Id { get; set; }
        public int EmployeeDni { get; set; }
        public EmployeeDTO? Employee { get; set; }
        public decimal SalaryAdvance { get; set; }
        public DateTime AdvanceDate { get; set; }
        public string Description { get; set; }

        public string SupervisorName {  get; set; }
        public int farmId { get; set; }

        public bool IsPaid { get; set; }

        public PaymentAdvanceDTO()
        {
            this.Employee = new EmployeeDTO();
            this.Description = string.Empty;
            this.SupervisorName = string.Empty;
            this.AdvanceDate = DateTime.Now;
        }
    }
}
