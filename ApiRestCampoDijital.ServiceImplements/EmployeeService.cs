using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Data.Repository;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using ApiRestCampoDijital.ServiceImplements.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFarmService _farmService;
        private readonly ICategoryService _categoryService;
        private IFarmSupervisorRepository _farmSupervisorRepository;
        public EmployeeService(IEmployeeRepository employeeRepository,
                               IFarmService farmService,
                               ICategoryService categoryService,
                               IFarmSupervisorRepository farmSupervisorRepository) {
                this._employeeRepository = employeeRepository;
            this._farmService = farmService;
            this._categoryService = categoryService;
            this._farmSupervisorRepository = farmSupervisorRepository;
        }

        public Task<bool> AddEmployee(int farmID,EmployeeDTO employeeDto)
        {
            //validar si el empleado existe en la finca
            bool employeeExistingInFarm = EmployeeExistingInFarm(employeeDto.Dni, farmID);
            if (employeeExistingInFarm)
                throw new Exception("Empleado ya existe en finca");
            //validar si empleado ya existe en base de datos
            int employeeExistingInDataBase = EmployeeExistingInDataBase(employeeDto.Dni);
            if (employeeExistingInDataBase != -1)
            {
                Employee employee = EmployeeMapper.GetEmployee(employeeDto);
                employee.Id = employeeExistingInDataBase;
                return this._employeeRepository.AddCategoriesToEmployeeExistingInDataBase(employee);
            }
            //validamos que exista la finca
            ValidateFarmExisting(farmID);
            //vaidar que employee tenga al menos 1 categoria
            ValidateIfCategoriesIsNull(employeeDto);
            //validar que categorias existan
            ValidateCategoriesExisting(farmID, employeeDto.Categories);
            //si empleado no existe en base de datos
            Employee employee1 = EmployeeMapper.GetEmployee(employeeDto);
            if (employee1.IsSupervisor)
            {
                //verificamos que supervisor no exista en ninguna finca
                var supervisorExisting = this._farmSupervisorRepository.FindFarmSupervisorByDni(employee1.Dni).Result;
                if (supervisorExisting != null)
                    throw new Exception("El empleado ya es supervisor en alguna finca");
                FarmSupervisor farmSupervisor = new FarmSupervisor(farmID,employee1.Dni);
                this._farmSupervisorRepository.AddFarmSupervisor(farmSupervisor);
            }
            return this._employeeRepository.AddEmployee(farmID,employee1);
        }

        private bool EmployeeExistingInFarm(int dni,int idFarm)
        {
           bool employeeExisting =  this._employeeRepository.IsEmployeeExistingInFarm(dni,idFarm).Result;
            
            return employeeExisting ;
        }

        private int EmployeeExistingInDataBase(int dni)
        {
            Employee employee = this._employeeRepository.IsEmployeeExistingInDataBase(dni).Result;

            return employee?.Id ?? -1;
          
        }

        private void ValidateFarmExisting(int idFarm)
        {
            var farm = this._farmService.FindFarmById(idFarm);
        }

       private void ValidateCategoriesExisting(int farmID,ICollection<CategoryDTO> categoryDTOs)
        {
            var findCategoriesExisting = this._categoryService.GetAllCategories(farmID).Result;
            var categoryExisting = categoryDTOs.All(categoryDTO => findCategoriesExisting.list.Any(c => c.Id==categoryDTO.Id)); 
            if(!categoryExisting)
                throw new Exception("Selecione categorias existentes");
        }

        private void ValidateIfCategoriesIsNull(EmployeeDTO employeeDTO)
        {
            if (employeeDTO.Categories == null || employeeDTO.Categories.Count==0)
                throw new Exception("Debe contener al menos 1 categoria");
        }

        public Task<bool> DeleteEmployee(int idEmployee,int idFarm)
        {
            return this._employeeRepository.DeleteEmployee(idEmployee,idFarm);
        }

        public Task<EmployeeDTO> EmployeeAutentication(long cuit, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmployeeModify(EmployeeDTO employeeDto)
        {
            //validamos si categoria es null
            ValidateIfCategoriesIsNull(employeeDto);
            //sacamos el id de finca
            int idFarm = employeeDto.Categories.ToArray().First().farmId;
            //EmployeeDTO findEmployeeData = FindEmployeeByDni(employeeDto.Dni,idFarm).Result;
            
            Employee employee1 = EmployeeMapper.GetEmployee(employeeDto);
            return this._employeeRepository.EmployeeModify(employee1,idFarm);
        }   
        public Task<EmployeeDTO> FindEmployeeByDni(int dni,int idFarm)
        {
           Employee employee = this._employeeRepository.FindEmployeeByDni(dni,idFarm).Result;
           EmployeeDTO employeeDTO = EmployeeMapper.GetEmployeeDTO(employee);
           return Task.FromResult(employeeDTO);
        }

        public Task<EmployeeDTO> FindEmployeeById(int employeeId)
        {
            Employee employee = this._employeeRepository.FindEmployeeById(employeeId).Result;
            EmployeeDTO employeeDTO = EmployeeMapper.GetEmployeeDTO(employee);
            return Task.FromResult(employeeDTO);
        }

        public PaginatedListting<EmployeeDTO> ListEmployees(string textoBusqueda,int idFarm)
        {
           
           PaginatedListting<Employee> paginatedListting = this._employeeRepository.ListEmployees(textoBusqueda,idFarm).Result;
           PaginatedListting<EmployeeDTO> paginatedListtingDTO = new PaginatedListting<EmployeeDTO>();
           paginatedListtingDTO.count = paginatedListting.count;
          // Console.WriteLine("en servicio es: " + paginatedListting.count);
           paginatedListtingDTO.list = EmployeeMapper.GetListEmployeeDTO(paginatedListting.list);

            Console.WriteLine("lista en servicio: " + paginatedListtingDTO.list.ToList().Count);
            return paginatedListtingDTO;
        }

        public async Task<PaginatedListting<EmployeeDTO>> ListClasificadorEmployees(int idFarm)
        {
            PaginatedListting<Employee> paginatedListting =await this._employeeRepository.ListClasificadorEmployees(idFarm);
            PaginatedListting<EmployeeDTO> paginatedListtingDTO = new PaginatedListting<EmployeeDTO>();
            paginatedListtingDTO.count = paginatedListting.count;
            paginatedListtingDTO.list = EmployeeMapper.GetListEmployeeDTO(paginatedListting.list);

            return paginatedListtingDTO;
        }

        public async Task<PaginatedListting<EmployeeDTO>> ListHourEmployees(int idFarm)
        {
            PaginatedListting<Employee> paginatedListting = await this._employeeRepository.ListHourEmployees(idFarm);
            PaginatedListting<EmployeeDTO> paginatedListtingDTO = new PaginatedListting<EmployeeDTO>();
            paginatedListtingDTO.count = paginatedListting.count;
            paginatedListtingDTO.list = EmployeeMapper.GetListEmployeeDTO(paginatedListting.list);

            return paginatedListtingDTO;
        }
    }
}
