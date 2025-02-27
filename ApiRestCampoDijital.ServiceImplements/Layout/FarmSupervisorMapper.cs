using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    public class FarmSupervisorMapper
    {
       public static FarmSupervisorDTO GetFarmSupervisorDTO(FarmSupervisor farmSupervisor)
        {
            return new FarmSupervisorDTO(farmSupervisor.farmId,farmSupervisor.dniSupervisor);
        }
    }
}
