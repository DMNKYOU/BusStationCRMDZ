using System.Collections.Generic;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models;


namespace BusStationCRM.BLL.Interfaces
{
    public interface IVoyagesService: IEntityService<Voyage>
    {
        Task<IEnumerable<Voyage>> Search(string search);
    }
}