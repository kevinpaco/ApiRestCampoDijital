using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model
{ 
    public class Employee:User
    {
        [Key]
        public int Id { get; set; }
        [Range(11111111,99999999,ErrorMessage ="El dni debe tener 8 dijitos")]
        [Required(ErrorMessage ="Ingrese el dni")]
        public int Dni { get; set; }
        [Required(ErrorMessage ="Elija si es supervisor")]
        public bool IsSupervisor { get; set; }
        [Required(ErrorMessage ="Ingrese al menos una categoria")]
        public ICollection<Category> Categories { get; set; }

        [ForeignKey(nameof(WorkingGroup))]
        [AllowNull]
        public int? WorkingGroupId { get; set; }
        public bool Deleted {  get; set; }
        public Employee()
        {
            this.Deleted = false;
            this.Categories = new List<Category>();
        }
    }
}
