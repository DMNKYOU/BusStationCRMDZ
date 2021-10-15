using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models;
using BusStationCRM.DAL.Contexts;
using BusStationCRM.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusStationCRM.DAL.Repositories
{
    public class OrdersRepository : BaseRepository<Order>
    {

        public OrdersRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new Task<Order> GetAsync(int id)
        {
            return _context.Orders.AsNoTracking()
                .Include(s => s.Voyage)
               // .Include(s => s.User)
                .SingleOrDefaultAsync(sr => sr.Id == id);
        }

        public new Task<List<Order>> GetAllAsync()
        {
            return _context.Orders
                .Include(s => s.Voyage)
                .ToListAsync();
        }
    }
}
