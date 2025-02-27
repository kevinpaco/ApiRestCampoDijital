using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name {get;set;}

        public ICollection<Employee> Employees { get; set; }

        public int farmId {  get; set; }
        [ForeignKey(nameof(farmId))]
        public Farm? farm { get;}

        public Category()
        {
            this.Name = string.Empty;
            this.Employees = new List<Employee>();
        }

        public Category(string name)
        {
            this.Name = name;
            this.Employees = new List<Employee>();
        }
    }
}
