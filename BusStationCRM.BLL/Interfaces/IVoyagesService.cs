using System.Collections.Generic;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models;
using BusStationCRM.BLL.Models.Search;


namespace BusStationCRM.BLL.Interfaces
{
    public interface IVoyagesService: IEntityService<Voyage>
    {
        Task<IEnumerable<Voyage>> Search(string search);
        Task<IEnumerable<Voyage>> Filter(VoyageFilter filter);
    }
}