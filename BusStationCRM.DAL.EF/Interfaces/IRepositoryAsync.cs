using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStationCRM.DAL.Interfaces
{
    public interface IRepositoryAsync<T> : IRepositoryBase<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task DeleteAsync(int id);
    }
}
