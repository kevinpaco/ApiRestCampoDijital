using ApiRestCampoDijital.Model.workingTime;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    public class FilterWorkingTimeMapper
    {
        public static FilterWorkingTime GetFilterWorkingTime(FilterWorkingTimeDTO filterWorkingTimeDTO)
        {
            FilterWorkingTime filterWorkingTime = new FilterWorkingTime()
            {
                startDate= filterWorkingTimeDTO.startDate,
                endDate= filterWorkingTimeDTO.endDate,
                 category= filterWorkingTimeDTO.category,
            };
            return filterWorkingTime;
        }
    }
}
