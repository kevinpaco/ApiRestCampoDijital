using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model
{
    public class History
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Ingrese la accion realizada por el usuario")]
        [MaxLength(1000,ErrorMessage ="LLego al maximos de la descripcion")]
        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public History()
        {
            this.Description = string.Empty;
            this.DateTime = DateTime.Now;
        }

    }
}
