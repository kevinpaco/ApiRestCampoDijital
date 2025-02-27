using ApiRestCampoDijital.Data.IRepository;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using ApiRestCampoDijital.ServiceImplements.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements
{
    public class FarmService : IFarmService
    {
        private readonly IFarmRepository farmRepository;

        public FarmService(IFarmRepository farmRepository)
        {
            this.farmRepository = farmRepository;
        }
        public async Task<bool> AddFarm(FarmDTO farmDTO)
        {
            Farm farm = FarmMapper.GetFarm(farmDTO);
            farm.Categories = LoadCategoriesForDefault();
            return await this.farmRepository.AddFarm(farm);
        }

        private List<Category> LoadCategoriesForDefault()
        {
            return new List<Category>() { new Category("Peon General"), new Category("Clasificador")};
        }

        public async Task<bool> DeleteFarmByID(int  idFarm)
        {
            Farm farm = await this.farmRepository.FindFarmById(idFarm);
            farm.Deleted = true;
            return await this.farmRepository.DeleteFarmByID(farm);
        }

        public Task<FarmDTO> FindFarmById(int idFarm)
        {
            Farm farm= this.farmRepository.FindFarmById(idFarm).Result;
            if(farm == null)
            {
                throw new Exception("Finca no Exite");
            }
            FarmDTO farmDTO = FarmMapper.GetFarmDTO(farm);
            return Task.FromResult(farmDTO);
        }

        public Task<PaginatedListting<FarmDTO>> FindFarmByIdEmployer(int idEmployer)
        {
            PaginatedListting<Farm> paginatedListting = this.farmRepository.FindFarmByIdEmployer(idEmployer).Result;
            PaginatedListting<FarmDTO> paginatedListtingDTO = new PaginatedListting<FarmDTO>();
            paginatedListtingDTO.count = paginatedListting.count;
            paginatedListtingDTO.list = FarmMapper.GetListFarmDTO(paginatedListting.list);
            return Task.FromResult(paginatedListtingDTO);
        }
    }
}
