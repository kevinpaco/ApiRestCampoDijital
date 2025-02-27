using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.Model.workingTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Data.IRepository
{
    public interface IWorkingTimeRepository<T>
    {
        Task<bool> AddWorkingTime(T workingTime);
        Task<bool> UpdateWorkingTime(T workingTime);
        Task<T> FindWorkingTimeById(int idWorkingTime);
        Task<PaginatedListting<T>> ListWorkingTime(DateTime dateTime,int idFarm);
        Task<PaginatedListting<T>> ListWorkingTimeNotPaid(FilterWorkingTime filterWorkingTime, int idFarm);
    }
}
