using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.Model.layout;

namespace ApiRestCampoDijital.Data.IRepository
{
    public interface IHistoryRepository
    {
        void AddHistory(History history);

        Task<History> FindHistoryById(int id);

        Task<PaginatedListting<History>> ListHitories(DateTime date);


    }
}
