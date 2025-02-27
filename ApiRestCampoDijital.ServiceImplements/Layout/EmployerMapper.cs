using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    public class EmployerMapper
    {
        public static EmployerDTO GetEmployerDTO(Employer employer)
        {
            var employerDto = new EmployerDTO();
            employerDto.Cuit = employer.Cuit;
            employerDto.Email = employer.Email;
            employerDto.Surname = employer.Surname;
            employerDto.Name = employer.Name;

            employerDto.Id = employer.Id;
            employerDto.Password = employer.Password;    
            return employerDto;
        }
        public static List<EmployerDTO> GetListEmployerDTO(IEnumerable<Employer> employers)
        {
           List<EmployerDTO> employerDTOs = new List<EmployerDTO>();
            foreach (var employer in employers)
            {
                var employerDto = new EmployerDTO();
                employerDto = GetEmployerDTO(employer);
                employerDTOs.Add(employerDto);
            }
            return employerDTOs;
        }

        public static Employer GetEmployer(EmployerDTO employerDto)
        {
            var employer = new Employer();
            employer.Cuit = employerDto.Cuit;
            employer.Email = employerDto.Email;
            employer.Surname = employerDto.Surname;
            employer.Name = employerDto.Name;
            employer.Id = employerDto.Id;
            employer.Password = Encrypt.GetSHA256(employerDto.Password);
            return employer;
        }
        public static List<Employer> GetListEmployer(IEnumerable<EmployerDTO> employerDTOs)
        {
            List<Employer> employers = new List<Employer>();
            foreach (var employer in employers)
            {
                var employerDto = new Employer();
                employer.Cuit = employerDto.Cuit;
                employer.Email = employerDto.Email;
                employer.Surname = employerDto.Surname;
                employer.Name = employerDto.Name;
                employer.Id = employerDto.Id;
                employers.Add(employer);
            }
            return employers;
        }
    }
}
