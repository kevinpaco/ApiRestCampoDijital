using ApiRestCampoDijital.Layout;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiRestCampoDijital.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("categorias")]
    [Authorize]
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost("agregar")]
        public IActionResult PostCategory([FromBody]CategoryDTO categoryDTO) {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this.categoryService.AddCategory(categoryDTO).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode =HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al agregar categoria");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
           return Ok(httpVMResponses);
        }

        [HttpPut("actualizar")]
        public IActionResult PutCategory([FromBody] CategoryDTO categoryDTO)
        {
            HttpVMResponses<bool> httpVMResponses = new HttpVMResponses<bool>();
            try
            {
                httpVMResponses.Datos = this.categoryService.UpdateCategory(categoryDTO).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = false;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al actualizar categoria");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }

        [HttpGet("lista")]
        public IActionResult GetListCategory(int idFarm)
        {
            var httpVMResponses = new HttpVMResponses<PaginatedListting<CategoryDTO>>();
            try
            {
                httpVMResponses.Datos = this.categoryService.GetAllCategories(idFarm).Result;
            }
            catch (Exception ex)
            {
                httpVMResponses.Datos = null;
                httpVMResponses.StatusCode = HttpStatusCode.InternalServerError;
                httpVMResponses.Messages.Add("Error al listar categorias");
                httpVMResponses.Messages.Add($"{ex.Message}");
            }
            return Ok(httpVMResponses);
        }
    }
}
