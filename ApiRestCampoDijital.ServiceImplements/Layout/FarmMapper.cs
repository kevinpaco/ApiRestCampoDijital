using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    public class FarmMapper
    {
        public static Farm GetFarm(FarmDTO farmDTO)
        {
            Farm farm = new Farm()
            {
                EmployerId = farmDTO.EmployerId,
                Name = farmDTO.Name, 
                Description = farmDTO.Description,
            };
            return farm;
        }

        public static FarmDTO GetFarmDTO(Farm farm)
        {
            FarmDTO farmDto = new FarmDTO()
            {
                Id = farm.Id,
                EmployerId = farm.EmployerId,
                EmployerDTO = EmployerMapper.GetEmployerDTO(farm.Employer),
                Name = farm.Name,
                Description = farm.Description,
            };
            return farmDto;
        }

        public static List<Farm> GetListFarm(List<FarmDTO> farmDTOs)
        {
            List<Farm> farms = new List<Farm>();
            foreach (FarmDTO f in farmDTOs)
            {
                var farm= GetFarm(f);
                farms.Add(farm);
            }
            return farms;
        }

        public static List<FarmDTO> GetListFarmDTO(List<Farm> farms)
        {
            List<FarmDTO> farmDTOs = new List<FarmDTO>();
            foreach (Farm f in farms)
            {
                var farm = GetFarmDTO(f);
                farmDTOs.Add(farm);
            }
            return farmDTOs;
        }
    }
}
