using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.IRepository
{
    public interface ICategoryRepository
    {
       Task<bool> AddCategory(Category category);

        Task<bool> UpdateCategory(Category category);

        Task<PaginatedListting<Category>> GetAllCategories(int idFarm);
    }
}
