using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    public class CategoryMapper
    {
        public static Category getCategory(CategoryDTO categoryDTO)
        {
            Category category = new Category()
            {
                Name = categoryDTO.Name,
                farmId = categoryDTO.farmId,
            };
            return category;
        }

        public static List<CategoryDTO> getListCategoryDTO(ICollection<Category> categories)
        {
            List<CategoryDTO> list = new List<CategoryDTO>();
            foreach (Category category in categories)
            {
                CategoryDTO categoryDTO = new CategoryDTO()
                {
                    Name = category.Name,
                    Id = category.Id,
                    farmId = category.farmId,
                };
                list.Add(categoryDTO);
            }
            return list;
        }

        public static List<Category> getListCategory(ICollection<CategoryDTO> categoryDTOs)
        {
            List<Category> list = new List<Category>();
            foreach (CategoryDTO categoryDTO in categoryDTOs)
            {
                Category category = new Category()
                {
                    Name = categoryDTO.Name,
                    Id = categoryDTO.Id,
                };
                list.Add(category);
            }
            return list;
        }
    }
}
