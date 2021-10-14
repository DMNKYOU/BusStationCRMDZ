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
    public class TicketsRepository : BaseRepository<Ticket>
    {

        public TicketsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new Task<Ticket> GetAsync(int id)
        {
            return _context.Tickets.AsNoTracking()
                .Include(s => s.Order)
                .Include(s => s.User)
                .SingleOrDefaultAsync(sr => sr.Id == id);
        }

        public new Task<List<Ticket>> GetAllAsync()
        {
            return _context.Tickets
                .Include(s => s.Order)
                .Include(s => s.User)
                .ToListAsync();
        }
    }
}
