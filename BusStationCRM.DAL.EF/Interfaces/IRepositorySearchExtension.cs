using BusStationCRM.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models.Search;

namespace BusStationCRM.DAL.Interfaces
{
    public interface IVoyagesRepository: IRepositoryAsync<Voyage>
    {
        Task<IEnumerable<Voyage>> Filter(VoyageFilter filter);
    }
}
