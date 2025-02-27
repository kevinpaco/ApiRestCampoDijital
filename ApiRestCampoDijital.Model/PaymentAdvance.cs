using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model
{
    public class PaymentAdvance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeDNI {  get; set; }

        [Required(ErrorMessage ="Ingrese el anticipo de pago")]  
        public decimal  SalaryAdvance {  get; set; }
        [Required(ErrorMessage ="Ingrese la fecha del anticipo")]  
        public DateTime AdvanceDate { get; set; }
        [AllowNull] 
        public string?  Description {  get; set; }

        public string SupervisorName {  get; set; }

        public int farmId { get; set; }
        public bool IsPaid {  get; set; }   
        public PaymentAdvance()
        {
            this.Description = string.Empty;
            this.SupervisorName = string.Empty;
            this.IsPaid = false;
        }
    }
}
