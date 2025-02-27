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
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository employeeRepository;

        public EmployerService(IEmployerRepository employerRepository)
        {
            this.employeeRepository = employerRepository;
        }

        public Task<bool> AddEmployer(EmployerDTO employerDTO)
        {
            Employer employer = EmployerMapper.GetEmployer(employerDTO);

            return employeeRepository.AddEmployer(employer);
        }

        public Task<bool> DeleteEmployer(long employerId)
        {
           return employeeRepository.DeleteEmployer(employerId);
        }

        public async Task<EmployerDTO> EmployerAutentication(UserAuthentication userAuthentication)
        {
            if (userAuthentication.dniOrCuit < 1111111111 || userAuthentication.dniOrCuit > 9999999999)
                throw new Exception("Eñ cuit debe tener 10 digitos");

            Employer employer =await this.employeeRepository.EmployerAutentication(userAuthentication.dniOrCuit);
            if (employer != null)
            {
                EmployerDTO employerDTO = EmployerMapper.GetEmployerDTO(employer);
                string password = Encrypt.GetSHA256(userAuthentication.password);
                return password.Equals(employerDTO.Password) ? employerDTO : null;
            }
            return null;
        }

        public Task<bool> EmployerModify(EmployerDTO employerDto)
        {
            Employer employer = EmployerMapper.GetEmployer(employerDto);
            return employeeRepository.EmployerModify(employer);
        }

        public Task<EmployerDTO> FindEmployerById(int employerId)
        {
            Employer employer = employeeRepository.FindEmployerById(employerId).Result;
            EmployerDTO employerDTO = EmployerMapper.GetEmployerDTO(employer);
            return Task.FromResult(employerDTO);
        }

        public Task<EmployerDTO> FindEmployerByCuit(long cuit)
        {
            Employer employer = employeeRepository.FindEmployerByCuit(cuit).Result;
            EmployerDTO employerDTO = EmployerMapper.GetEmployerDTO(employer);
            return Task.FromResult(employerDTO);
        }

        public PaginatedListting<EmployerDTO> ListEmployers(long cuit)
        {
           PaginatedListting<Employer> paginatedListting=  employeeRepository.ListEmployers(cuit).Result;
           PaginatedListting<EmployerDTO> paginatedListtingDTO = new PaginatedListting<EmployerDTO>();
           paginatedListtingDTO.count = paginatedListting.count;
           paginatedListtingDTO.list = EmployerMapper.GetListEmployerDTO(paginatedListting.list); 
           return paginatedListtingDTO;
        }
    }
}
