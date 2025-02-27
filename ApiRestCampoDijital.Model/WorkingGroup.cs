using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model
{
    public class WorkingGroup
    {   
        [Key]
        public int id {  get; set; }
        public int numero { get; set; }

        public int FarmId {  get; set; }
        public List<int> employees { get; set; }

        public WorkingGroup()
        {
            this.employees= new List<int>();
        }

    }
}
