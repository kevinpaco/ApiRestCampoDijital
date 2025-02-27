using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRestCampoDijital.ModelDTO;

namespace ApiRestCampoDijital.Service
{
    public interface IEmployerService
    {
        Task<bool> AddEmployer(EmployerDTO employerDTO);
        Task<EmployerDTO> EmployerAutentication(UserAuthentication userAuthentication);
        Task<bool> EmployerModify(EmployerDTO employerDto);
        Task<EmployerDTO> FindEmployerById(int employerId);
        Task<EmployerDTO> FindEmployerByCuit(long cuit);
        Task<bool> DeleteEmployer(long employerId);
        PaginatedListting<EmployerDTO> ListEmployers(long cuit);

    }
}
