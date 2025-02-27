using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.ServiceImplements.Layout
{
    public class HistoryMapper
    {
        public static HistoryDTO GetHistoryDTO(History history)
        {
            HistoryDTO historyDTO = new HistoryDTO()
            {
                Id = history.Id,
                Description = history.Description,
                DateTime = history.DateTime,

            };
            return historyDTO;
        }

        public static List<HistoryDTO> GetListHistoryDTO(IEnumerable<History> histories)
        {
            List<HistoryDTO> historyDTOs = new List<HistoryDTO>();
            foreach (History history in histories) {
                HistoryDTO historyDTO = new HistoryDTO()
                {
                    Id = history.Id,
                    Description = history.Description,
                    DateTime = history.DateTime,
                };
               historyDTOs.Add(historyDTO);
            }
           return historyDTOs;
        }

        public static History GetHistory(HistoryDTO historyDto)
        {
            History history = new History()
            {
                Description = historyDto.Description,
            };
            return history;
        }

        public static List<History> GetListHistoryDTO(IEnumerable<HistoryDTO> historyDTOs)
        {
            List<History> histories = new List<History>();
            foreach (HistoryDTO historyDto in historyDTOs)
            {
                History history = new History()
                {
                    Description = historyDto.Description,
                };
               histories.Add(history);
            }
            return histories;
        }
    }
}
