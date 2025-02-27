using ApiRestCampoDijital.Controllers;
using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Data.Repository;
using ApiRestCampoDijital.Layout;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Service;
using ApiRestCampoDijital.ServiceImplements;
using Microsoft.AspNetCore.Mvc;

namespace TestCampoDigital
{
    [TestClass]
    public sealed class Test1
    {
        [Ignore]
        public void TestMethod1()
        {
            // Configurar el repositorio y el servicio
            IEmployerRepository employerRepository = new EmployerRepository();
            IEmployerService employerService = new EmployerService(employerRepository);
            EmployerController employerController = new EmployerController(employerService);

            // Llamar al método del controlador
            IActionResult actionResult = employerController.GetListEmployers(32323);

            // Convertir el resultado a OkObjectResult
            var okResult = actionResult as OkObjectResult;
            Assert.IsNotNull(okResult);

            // Convertir el valor del resultado a HttpVMResponses<PaginatedListting<Employer>>
            var httpVMResponses = okResult.Value as HttpVMResponses<PaginatedListting<Employer>>;
            Assert.IsNotNull(httpVMResponses);

            // Acceder a los datos y realizar las aserciones
            var paginatedListting = httpVMResponses.Datos;
           // Console.WriteLine( paginatedListting.list.Count);
            Assert.IsNotNull(paginatedListting);
           // Assert.AreEqual(2, paginatedListting.list.Count);

            // Imprimir los datos para verificación
           // Console.WriteLine("ess: " + paginatedListting.list.Count);
        }

      /*  [TestMethod]
        public void TestMethod2()
        {
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            IEmployeeService emp = new EmployeeService(employeeRepository);

            var t = emp.ListEmployees("");
            Assert.AreEqual(2, t.Result.count);
        }*/

    }
}
