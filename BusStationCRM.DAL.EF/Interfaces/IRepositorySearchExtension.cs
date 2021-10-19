using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusStationCRM.DAL.Interfaces
{
    public interface IRepositorySearchExtension<T>: IRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> Filter(object filter);
    }
}
