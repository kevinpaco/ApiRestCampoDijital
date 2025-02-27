using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.IRepository
{
    public interface IWorkingGroupRepository
    {
        Task<PaginatedListting<Employee>> getGroupEmployee(int numberGroup, int idFarm);
    }
}
