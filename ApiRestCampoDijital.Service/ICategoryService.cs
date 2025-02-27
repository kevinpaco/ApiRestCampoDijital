using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Service
{
    public interface ICategoryService
    {
        Task<bool> AddCategory(CategoryDTO categoryDTO);

        Task<bool> UpdateCategory(CategoryDTO categoryDTO);

        Task<PaginatedListting<CategoryDTO>> GetAllCategories(int idFarm);
    }
}
