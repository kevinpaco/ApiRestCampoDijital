using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model.workingTime
{
    public class WorkingTimeForKilogram:WorkingTime
    {
        [Required(ErrorMessage ="Ingrese el numero de kilos")]
        [Range(0,999999)]
        public decimal KilogramsNumber { get; set; }
        [ForeignKey(nameof(WorkingTimeInGroup))]
        public int WorkingTimeGroupID { get; set; }

        [ForeignKey(nameof(WorkingTimeInGroup))]
        public int WorkingTimeInGroupId { get; set; }
    }
}
