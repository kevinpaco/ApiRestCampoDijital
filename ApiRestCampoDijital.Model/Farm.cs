using ApiRestCampoDijital.Model.workingTime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model
{
    public class Farm
    {
        [Key]
        public int Id {  get; set; }
        [Required(ErrorMessage ="Ingrese nombre de la finca")]
        [MaxLength(60,ErrorMessage ="El nombre debe tener maximo 60 letras"),MinLength(3,ErrorMessage ="El nombre debe tener un minimo de 3 letras")]
        public string Name { get; set; }
        [Required]
        public int EmployerId {  get; set; }
        [ForeignKey(nameof(EmployerId))]
        [Required(ErrorMessage ="Ingrese el empleador")]
        public Employer  Employer {  get; set; }
        [AllowNull]
        public ICollection<WorkingTime> WorkingTimes {  get; set; }

        [AllowNull]
        public ICollection<WorkingGroup> WorkingGroups { get; set; }

        public ICollection<Category> Categories { get; set; }
        [AllowNull]
        public string Description {  get; set; }
        public bool Deleted {  get; set; }

        public Farm()
        {
            this.WorkingGroups= new List<WorkingGroup>();
            this.Categories= new List<Category>();
            this.Deleted = false;
            this.Name= string.Empty;
        }
    }
}
