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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public Task<bool> AddCategory(CategoryDTO categoryDTO)
        {
            Category category = CategoryMapper.getCategory(categoryDTO);
            return this.categoryRepository.AddCategory(category);
        }

        public Task<PaginatedListting<CategoryDTO>> GetAllCategories(int idFarm)
        {
            PaginatedListting<Category> paginatedListting = this.categoryRepository.GetAllCategories(idFarm).Result;

            PaginatedListting<CategoryDTO> paginatedListtingDTO = new PaginatedListting<CategoryDTO>();
            paginatedListtingDTO.count = paginatedListtingDTO.count;
            paginatedListtingDTO.list = CategoryMapper.getListCategoryDTO(paginatedListting.list);

            return Task.FromResult(paginatedListtingDTO);
        }

        public Task<bool> UpdateCategory(CategoryDTO categoryDTO)
        {
            Category category = CategoryMapper.getCategory(categoryDTO);
            return this.categoryRepository.UpdateCategory(category);
        }
    }
}
