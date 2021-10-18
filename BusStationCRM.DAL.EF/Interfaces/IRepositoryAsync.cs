using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStationCRM.DAL.Interfaces
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<IAsyncEnumerable<T>> FindAsync(Func<T, Boolean> predicate);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task<T> GetAsync(int id);
        Task DeleteAsync(int id);
    }
}
