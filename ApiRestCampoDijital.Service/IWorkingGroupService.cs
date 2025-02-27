using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Service
{
    public interface IWorkingGroupService
    {
       Task<bool> UpdateEmployeeWitchWorkingGroup(List<int> dnis,int farmId, int numeroWokingGroup);
        Task<PaginatedListting<EmployeeDTO>> getGroupEmployee(int numberGroup, int idFarm);
    }
}
