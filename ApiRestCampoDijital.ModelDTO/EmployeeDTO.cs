using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ModelDTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        
        public int Dni { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public bool IsSupervisor { get; set; }
        public ICollection<CategoryDTO>? Categories { get; set; }
        public int WorkingGroupId { get; set; }

        public EmployeeDTO()
        {
            Dni = 0;
            Name = string.Empty;
            Surname = string.Empty;
            Password = string.Empty;
            Categories = new List<CategoryDTO>();
            WorkingGroupId = 0;
            IsSupervisor = false;
        }

        public override string ToString()
        {
            return "dni: " + Dni + ", name:" + Name + ", farm id: ";
        }

    }
}
