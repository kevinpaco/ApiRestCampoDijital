using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ModelDTO
{
    public class FarmDTO
    {
        public int Id { get; set; }  
        public string Name { get; set; }
        public int EmployerId { get; set; }
        public EmployerDTO? EmployerDTO { get; set; }   
        public ICollection<WorkingTimeDTO> WorkingTimeDTOs { get; }  
        public ICollection<WorkingGroupDTO> WorkingGroupDTOs { get;  }   
        public ICollection<CategoryDTO> CategoryDTOs { get;  }
        public string Description { get; set; }

        public FarmDTO()
        {
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.EmployerDTO = new EmployerDTO();
        }
    }
}
