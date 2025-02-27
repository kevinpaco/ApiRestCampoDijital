using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Service
{
    public interface IFarmSupervisorService
    {

        Task<FarmSupervisorDTO> EmployeeAutentication(UserAuthentication userAuthentication);
    }
}
