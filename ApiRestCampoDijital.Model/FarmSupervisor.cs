using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model
{
    public class FarmSupervisor
    {
        [Key]
        public int id {  get; set; }
        public int farmId { get; set; }
        public int dniSupervisor {  get; set; }

        public FarmSupervisor(int farmId,int dniSupervisor) {
           this.farmId = farmId;
           this.dniSupervisor = dniSupervisor;
        } 
    }
}
