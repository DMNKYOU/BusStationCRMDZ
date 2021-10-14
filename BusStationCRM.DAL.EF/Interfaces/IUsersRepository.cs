using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationCRM.DAL.Interfaces
{
    public interface IUsersRepository<T> where T : class
    {
        Task<T> GetAsync(string id);
        Task<List<T>> GetAllAsync();
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(string id);
    }
}
