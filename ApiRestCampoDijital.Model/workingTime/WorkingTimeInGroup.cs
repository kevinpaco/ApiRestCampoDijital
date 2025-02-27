using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiRestCampoDijital.Model.workingTime
{
    public class WorkingTimeInGroup
    {
        [Key]
      public int Id { get; set; }
        [Required(ErrorMessage ="Ingrese la cargoria del grupo")]
        [MaxLength(20,ErrorMessage = "Maximo de letras 20"),MinLength(1,ErrorMessage ="Minimo de letras 1")]
      public string CategoryGroup { get; set; }
        [Required]
      public DateTime Date { get; set; }
        [Required]
      public List<WorkingTimeForKilogram> WorkingTimes { get; set; }
        [Required]
      public int EmployeePresent { get; set; }
        [AllowNull]
      public int BalesNumber { get; set; }
        [Required]
      public decimal TotalKilograms { get; set; }
        [Required]
      public string AttendantName { get; set; }
        [Required]
      public int NumberGroup { get; set; }

      public int? farmId {  get; set; }

        public WorkingTimeInGroup()
        {
            this.WorkingTimes = new List<WorkingTimeForKilogram>();
            this.AttendantName = string.Empty;
            this.CategoryGroup = "Cinteros";
        }

        public override string ToString()
        {
            return $"cat: {CategoryGroup}, date:{Date}, presentes: {EmployeePresent}, kilos: {TotalKilograms}, presentes: {WorkingTimes.Count()}";
        }

    }
}
