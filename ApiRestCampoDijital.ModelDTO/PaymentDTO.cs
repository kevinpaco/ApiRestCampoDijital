using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ModelDTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int EmployeeDni { get; set; }
        public string EmployeeName { get; set; }
        public EmployeeDTO Employee { get; set; }
        //public string EmployeeName { get; set; }    
        public int EmployerId { get; set; }
        public string EmployerName { get; set; }
        public List<WorkingTimeDTO> WorkingTimes { get; set; }
        public decimal TotalSalary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string State { get; set; }

        public string Category { get; set; }

        public int? HoursNumber {  get; set; }

        public double? KilogramNumber { get; set; }

        public int FarmId { get; set; }

        public PaymentDTO()
        {
            this.Employee = new EmployeeDTO();
            this.WorkingTimes = new List<WorkingTimeDTO>();
            this.State = string.Empty;
           // this.EmployeeName = string.Empty;
            this.EmployerName = string.Empty;
            this.Category= string.Empty;
        }
    }
}
