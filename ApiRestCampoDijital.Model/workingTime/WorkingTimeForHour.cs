using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model.workingTime
{
    public class WorkingTimeForHour:WorkingTime
    {
        [Required(ErrorMessage ="Ingrese el numero de horas")]
        [Range(0,23)]
        public int HourNumber {  get; set; }

    }
}
