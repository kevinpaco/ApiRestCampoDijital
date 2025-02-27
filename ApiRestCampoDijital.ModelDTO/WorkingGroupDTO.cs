using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ModelDTO
{
    public class WorkingGroupDTO
    {
        public int id { get; set; }
        public int numero { get; set; }
        public List<int> employees { get; set; }

        public WorkingGroupDTO()
        {
            this.employees = new List<int>();
        }
    }
}
