using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    public class WorkingGroupMapper
    {
        public static WorkingGroup GetWorkingGroup(WorkingGroupDTO workingGroupDTO)
        {
            WorkingGroup workingGroup = new WorkingGroup()
            {
                employees = workingGroupDTO.employees,
                numero = workingGroupDTO.numero,
            };
            return workingGroup;
        }
    }
}
