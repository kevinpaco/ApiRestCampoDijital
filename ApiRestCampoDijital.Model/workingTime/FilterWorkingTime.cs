using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model.workingTime
{
    public class FilterWorkingTime
    {
        public DateTime startDate { set; get; }
        public DateTime endDate { set; get; }
        public int EmployeeId { set; get; }
        public string category { set; get; }

        public FilterWorkingTime()
        {
            this.category=string.Empty;
        }
    }
}
