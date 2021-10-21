using System.Collections.Generic;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models;


namespace BusStationCRM.BLL.Interfaces
{
    public interface IOrdersService : IEntityService<Order>
    {
        Task AddOrderAndTicket(Order entity);
    }
}