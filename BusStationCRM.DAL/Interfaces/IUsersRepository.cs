using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models;

namespace BusStationCRM.DAL.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetAsync(string id);
        Task<List<User>> GetAllAsync();
        IEnumerable<User> Find(Func<User, Boolean> predicate);
        Task CreateAsync(User item);
        Task UpdateAsync(User item);
        Task DeleteAsync(string id);
    }
}
