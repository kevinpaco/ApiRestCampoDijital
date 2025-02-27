using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    internal class EmployeeMapper
    {
        public static EmployeeDTO GetEmployeeDTO(Employee employee)
        {
            var employeeDto = new EmployeeDTO();
            employeeDto.Dni = employee.Dni;
            employeeDto.Surname = employee.Surname;
            employeeDto.Name = employee.Name;
            employeeDto.Id = employee.Id;
            employeeDto.Password = employee.Password;
            employeeDto.IsSupervisor = employee.IsSupervisor;
            employeeDto.Categories = CategoryMapper.getListCategoryDTO(employee.Categories);
            return employeeDto;
        }
        public static List<EmployeeDTO> GetListEmployeeDTO(IEnumerable<Employee> employees)
        {
            List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
            foreach (var employee in employees)
            {
                EmployeeDTO employeeDTO = GetEmployeeDTO(employee);

                employeeDTOs.Add(employeeDTO);
            }
            return employeeDTOs;
        }

        public static Employee GetEmployee(EmployeeDTO employeeDto)
        {
            var employee = new Employee();
            employee.Surname = employeeDto.Surname;
            employee.Name = employeeDto.Name;
            employee.Id = employeeDto.Id;
            employee.Dni = employeeDto.Dni;
            employee.IsSupervisor = employeeDto.IsSupervisor;
            if (employeeDto.IsSupervisor)
                 employee.Password = Encrypt.GetSHA256(employeeDto.Password);
            employee.IsSupervisor = employeeDto.IsSupervisor;
            employee.Categories = CategoryMapper.getListCategory(employeeDto.Categories);
            employee.WorkingGroupId = employeeDto.WorkingGroupId;
            return employee;
        }
        public static List<Employee> GetListEmployee(IEnumerable<EmployeeDTO> employeeDTOs)
        {
            List<Employee> employees = new List<Employee>();
            foreach (var employeeDto in employeeDTOs)
            {
                var employee = new Employee();
                employee.Surname = employeeDto.Surname;
                employee.Name = employeeDto.Name;
                employee.Id = employeeDto.Id;               
                employee.Dni = employeeDto.Dni;
                employee.Password = employeeDto.Password;
                employee.IsSupervisor = employeeDto.IsSupervisor;
            }
            return employees;
        }
    }
}
