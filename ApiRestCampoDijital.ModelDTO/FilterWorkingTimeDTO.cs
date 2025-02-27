using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ModelDTO
{
    public class FilterWorkingTimeDTO
    {
        public DateTime startDate { set; get; }
        public DateTime endDate { set; get; }
        public int EmployerDNI { set; get; }
        public string category { set; get; }

        public FilterWorkingTimeDTO()
        {
            this.category = string.Empty;
        }

        public override string ToString()
        {
            return $"startDate: {this.startDate}, endDate: {this.endDate}, dni: {this.EmployerDNI}, category: {this.category}";
        }
    }
}
