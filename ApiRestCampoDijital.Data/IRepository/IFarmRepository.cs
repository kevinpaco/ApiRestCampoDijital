using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.IRepository
{
    public interface IFarmRepository
    {
        Task<bool> AddFarm(Farm farm);

        Task<PaginatedListting<Farm>> FindFarmByIdEmployer(int idEmployer);

        Task<Farm> FindFarmById(int idFarm);

        Task<bool> DeleteFarmByID(Farm farm);

    }
}
