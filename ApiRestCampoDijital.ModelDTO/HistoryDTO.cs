using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ModelDTO
{
    public class HistoryDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public DateTime? DateTime { get; set; }

        public HistoryDTO()
        {
            this.Description = string.Empty;
        }
    }
}
