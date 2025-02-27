using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model.workingTime
{
    public class WorkingTime
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Ingrese fecha de Jornada actual")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage ="Ingrese el id de empleado")]
        public int EmployeeId {  get; set; }
        public Employee? Employee { get; set; }
        [Required(ErrorMessage ="Ingrese el nombre del supervisor")]
        [MaxLength(30,ErrorMessage ="El nombre no debe exeder las 30 palabras"),MinLength(2,ErrorMessage ="El nombre debe tener 3 letras como minimo")]
        public string AttendantName { get; set; }
        [Required(ErrorMessage ="Ingrese la asistencia")]
        public bool Present { get; set; }
        [Required]
        public bool Paid { get; set; }
        [Required(ErrorMessage ="Ingrese la categoria de trabajo")]
        public string Category { get; set; }

        public int? PaymentId { get; set; }
         
        public int FarmId { get; set; }

        public WorkingTime()
        {
            Paid = false;
            Present = false;
            Category = string.Empty;
            AttendantName = string.Empty;
        }
    }
}
