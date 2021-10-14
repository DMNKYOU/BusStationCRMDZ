using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStationCRM.DAL.Interfaces
{
    public interface IUsersRepository<T>: IRepositoryBase<T> where T : class
    {
        Task<T> GetAsync(string id);
        Task DeleteAsync(string id);
    }
}
