using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.IRepository
{
    public interface IEmployerRepository
    {
        Task<bool> AddEmployer(Employer employer);
        Task<Employer> EmployerAutentication(long cuit);
        Task<bool> EmployerModify(Employer employer);
        Task<Employer> FindEmployerById(int employerId);
        Task<Employer> FindEmployerByCuit(long cuit);
        Task<bool> DeleteEmployer(long employerId);
        Task<PaginatedListting<Employer>> ListEmployers(long cuit);

    }
}
