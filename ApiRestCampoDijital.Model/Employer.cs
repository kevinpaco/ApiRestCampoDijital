using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model
{
    public class Employer:User
    {
        [Key]
        public int Id { get; set;}
        [Required(ErrorMessage ="Ingrese el cuit")]
        public long Cuit { get; set; }
       [AllowNull]
        public string Email {get;set;}
        public bool Deleted {get;set;}

        public Employer()
        {
            this.Deleted = false;
        }
    }
}
