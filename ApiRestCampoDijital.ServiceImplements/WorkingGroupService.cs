using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
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
    public class WorkingGroupService : IWorkingGroupService
    {
        private readonly IWorkingGroupRepository repository;
        private readonly IEmployeeService employeeService;

        public WorkingGroupService(IWorkingGroupRepository workingGroupRepository, IEmployeeService employeeService)
        {
            this.repository = workingGroupRepository;
            this.employeeService = employeeService;
        }

        public async Task<PaginatedListting<EmployeeDTO>> getGroupEmployee(int numberGroup, int idFarm)
        {
            PaginatedListting<Employee> paginatedListting =await this.repository.getGroupEmployee(numberGroup,idFarm);
            PaginatedListting<EmployeeDTO> paginatedListtingDTO = new PaginatedListting<EmployeeDTO>();
            paginatedListtingDTO.count = paginatedListting.count;
            paginatedListtingDTO.list = EmployeeMapper.GetListEmployeeDTO(paginatedListting.list);
            return paginatedListtingDTO;
        }

        public async Task<bool> UpdateEmployeeWitchWorkingGroup(List<int> dnis,int farmId,int numeroWokingGroup)
        {
           int resultValidateDni = ValidateDNIs(dnis);
            if (resultValidateDni==-1)
            {
                foreach (int dni in dnis)
                {
                    EmployeeDTO employeeDTO = await this.employeeService.FindEmployeeByDni(dni,farmId);
                    if (employeeDTO != null)
                    {
                        employeeDTO.WorkingGroupId = numeroWokingGroup;
                        await this.employeeService.EmployeeModify(employeeDTO);
                    }
                    else
                    {
                        throw new Exception("Error al asignar el id del grupo de trabajo a un empleado");
                    }
                }
            }
            else
            {
                throw new Exception("Error: Los DNIs deben tener 8 dijitos, dni invalido: " + dnis[resultValidateDni]);
            }
            return true;
        }

        private int ValidateDNIs(List<int> dnis)
        {
            for (int i = 0; i < dnis.Count; i++)
            {
                int dni=dnis[i];
                if (dni < 11111111 || dni > 99999999)
                    return i;
            }
            return -1;
        }
    }
}
