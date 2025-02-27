using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ModelDTO
{
    public class WorkingTimeDTO
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDTO? Employee { get; set; }
        public string? AttendantName { get; set; }
        public bool Present { get; set; }
        public bool Paid { get; set; }
        public string? Category { get; set; }
        public int? PaymentId { get; set; }

        public int FarmId { get; set; }

        public int? HourNumber { get; set; }
        public decimal? KilogramsNumber { get; set; }
        public int? WorkingTimeGroupID { get; set; }

        public int WorkingTimeInGroupId { get; set; }

        public WorkingTimeDTO()
        {
            Paid = false;
            Present = false;
            Category = string.Empty;
            AttendantName = string.Empty;
            this.Employee = new EmployeeDTO();
        }
    }
}
