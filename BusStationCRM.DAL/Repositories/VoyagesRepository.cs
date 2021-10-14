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
    public class VoyagesRepository : BaseRepository<Voyage>
    {

        public VoyagesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new Task<Voyage> GetAsync(int id)
        {
            return _context.Voyages.AsNoTracking()
                .Include(v => v.BusStops)
                .Include(v=>v.BusStopArrival)
                .Include(v=>v.BusStopDeparture)
                .Include(v=>v.Orders)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public new Task<List<Voyage>> GetAllAsync()
        {
            return _context.Voyages
                .Include(s => s.BusStops)
                .ToListAsync();
        }
    }
}
