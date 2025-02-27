using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using ApiRestCampoDijital.ServiceImplements.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements
{
    public class FarmSupervisorService : IFarmSupervisorService
    {
        private readonly IFarmSupervisorRepository farmSupervisorRepository;
        private readonly IEmployeeService employeeService;

        public FarmSupervisorService(IFarmSupervisorRepository farmSupervisorRepository,
                                     IEmployeeService employeeService)
        {
            this.farmSupervisorRepository = farmSupervisorRepository;
            this.employeeService = employeeService;
        }

        public async Task<FarmSupervisorDTO> EmployeeAutentication(UserAuthentication userAuthentication)
        {
            if (userAuthentication==null || (userAuthentication.dniOrCuit < 11111111 || userAuthentication.dniOrCuit > 99999999))
                throw new Exception("El dni debe tener 8 dijitos");
            int dni = (int)userAuthentication.dniOrCuit;
            FarmSupervisor farmSupervisorExisting =await this.farmSupervisorRepository.FindFarmSupervisorByDni(dni);

            if (farmSupervisorExisting != null)
            {
              EmployeeDTO employeeDTO=await  this.employeeService.FindEmployeeByDni(farmSupervisorExisting.dniSupervisor,farmSupervisorExisting.farmId);
                string passwordAuthentication = Encrypt.GetSHA256(userAuthentication.password);
                return passwordAuthentication.Equals(employeeDTO.Password) ? FarmSupervisorMapper.GetFarmSupervisorDTO(farmSupervisorExisting) : null ;
            }
            return null;     
        }
    }
}
