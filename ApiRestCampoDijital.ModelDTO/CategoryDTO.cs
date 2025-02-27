using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ModelDTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

       // public ICollection<EmployeeDTO>? Employees { get;}

        public int farmId {  get; set; }
        //public FarmDTO? FarmDTO { get;}

        public CategoryDTO()
        {
            this.Name = string.Empty;
         //   this.Employees = new List<EmployeeDTO>();
        }
    }
}
