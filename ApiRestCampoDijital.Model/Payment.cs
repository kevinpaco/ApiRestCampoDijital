using ApiRestCampoDijital.Model.workingTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EmployeeDni { get; set; }
        public string? EmployeeName {  get; set; }
        [Required]
        public int EmployerId {  get; set; }
        public string? EmployerName { get; set; }

        [Required]
        public List<WorkingTime>? WorkingTimes {  get; set; }
        [Required(ErrorMessage ="Ingrese el Salario total")]
        [Range(0,99999999.0)]
        public decimal TotalSalary {  get; set; }
        [Required(ErrorMessage = "Ingrese la fecha de inicio")]
        public DateTime StartDate {  get; set; }
        [Required(ErrorMessage = "Ingrese la fecha de pago")]
        public DateTime PaymentDate {  get; set; }

        public string State {  get; set; }

        public string Category { get; set; }

        public int? HoursNumber { get; set; }

        public double? KilogramNumber { get; set; }
        public int FarmId {  get; set; }
        public Payment()
        {
            this.WorkingTimes = new List<WorkingTime>();
            this.State = "pagado";
            this.Category = string.Empty;
        }
    }
}
