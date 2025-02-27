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
    public interface IHistoryService
    {
        void AddHistory(HistoryDTO historyDTO);

        Task<HistoryDTO> FindHistoryById(int id);

        Task<PaginatedListting<HistoryDTO>> ListHitories(DateTime date);
    }
}
