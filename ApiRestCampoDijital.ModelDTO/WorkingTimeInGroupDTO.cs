using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ModelDTO
{
    public class WorkingTimeInGroupDTO
    {
        public int Id { get; set; }
        public string CategoryGroup { get; }
        public DateTime Date { get; set; }
        public List<WorkingTimeDTO>? WorkingTimes { get; set; }
        public int EmployeePresent { get; set; }
        public int BalesNumber { get; set; }
        public decimal TotalKilograms { get; set; }
        public string AttendantName { get; set; }
        public int NumberGroup { get; set; }

        public int farmId { get; set; }

        public WorkingTimeInGroupDTO()
        {
            this.WorkingTimes = new List<WorkingTimeDTO>();
            this.AttendantName = string.Empty;
            this.CategoryGroup = "Cinteros";
        }

        public override string ToString()
        {
            return $"cat: {CategoryGroup}, date:{Date}, presentes: {EmployeePresent}, kilos: {TotalKilograms}, presentes: {WorkingTimes.Count()}";
        }

    }
}
