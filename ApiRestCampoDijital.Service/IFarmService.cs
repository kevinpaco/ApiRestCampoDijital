using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRestCampoDijital.ModelDTO;

namespace ApiRestCampoDijital.Service
{
    public interface IFarmService
    {
        Task<bool> AddFarm(FarmDTO farmDTO);

        Task<PaginatedListting<FarmDTO>> FindFarmByIdEmployer(int idEmployer);

        Task<FarmDTO> FindFarmById(int idFarm);

        Task<bool> DeleteFarmByID(int  idFarm);
    }
}
