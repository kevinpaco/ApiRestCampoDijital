using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ModelDTO
{
    public class FarmSupervisorDTO
    {
        public int id { get; set; }
        public int farmId { get; set; }
        public int dniSupervisor { get; set; }

        public FarmSupervisorDTO(int farmId, int dniSupervisor)
        {
            this.farmId = farmId;
            this.dniSupervisor = dniSupervisor;
        }
    }
}
