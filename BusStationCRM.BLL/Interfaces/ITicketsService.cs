using System.Collections.Generic;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models;


namespace BusStationCRM.BLL.Interfaces
{
    public interface ITicketsService: IEntityService<Ticket>
    {
        Task UpdateStatusTicketOrder(Ticket ticket);
    }
}