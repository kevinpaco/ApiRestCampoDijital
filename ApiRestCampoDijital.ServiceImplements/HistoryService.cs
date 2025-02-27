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
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository historyRepository;

        public HistoryService(IHistoryRepository historyRepository)
        {
            this.historyRepository = historyRepository;
        }
        public void AddHistory(HistoryDTO historyDTO)
        {
           History history1 = HistoryMapper.GetHistory(historyDTO);
           this.historyRepository.AddHistory(history1);
        }

        public Task<HistoryDTO> FindHistoryById(int id)
        {
            History history = this.historyRepository.FindHistoryById(id).Result;
            HistoryDTO historyDTO = HistoryMapper.GetHistoryDTO(history);
            return Task.FromResult(historyDTO);
        }

        public Task<PaginatedListting<HistoryDTO>> ListHitories(DateTime date)
        {
            PaginatedListting<History> paginatedListting = this.historyRepository.ListHitories(date).Result;
            PaginatedListting<HistoryDTO> paginatedListtingDTO = new PaginatedListting<HistoryDTO>();
            paginatedListtingDTO.count = paginatedListting.count;
            paginatedListtingDTO.list = HistoryMapper.GetListHistoryDTO(paginatedListting.list);
            return  Task.FromResult( paginatedListtingDTO);

        }
    }
}
