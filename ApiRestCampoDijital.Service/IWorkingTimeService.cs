using ApiRestCampoDijital.Model.layout;
using ApiRestCampoDijital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Service
{
    public interface IWorkingTimeService<T>
    {
        Task<bool> AddWorkingTime(T workingTimeDTO);
        Task<bool> UpdateWorkingTime(T workingTimeDTO);
        Task<T> FindWorkingTimeById(int idWorkingTime);
        Task<PaginatedListting<T>> ListWorkingTime(DateTime dateTime, int idFarm);
        Task<PaginatedListting<T>> ListWorkingTimeNotPaid(FilterWorkingTimeDTO filterWorkingTimeDTO, int idFarm);
    }
}
