using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model.layout
{
    public class PaginatedListting<T>
    {
        public int count { get; set; }
        public List<T> list { get; set; }

        public PaginatedListting()
        {
            this.count = 0;
            this.list = new List<T>();
        }
    }
}
