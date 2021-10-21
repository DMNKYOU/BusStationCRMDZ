using System.Collections.Generic;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models;


namespace BusStationCRM.BLL.Interfaces
{
    public interface IBusStopsService: IEntityService<BusStop>
    {
        Task<IEnumerable<BusStop>> Search(string search);
    }
}