using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models;
using BusStationCRM.BLL.Models.Search;
using BusStationCRM.DAL.Contexts;
using BusStationCRM.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusStationCRM.DAL.Repositories
{
    public class VoyagesRepository : BaseRepository<Voyage>, IVoyagesRepository
    {

        public VoyagesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Task<Voyage> GetAsync(int id) 
        {
            return _context.Voyages.AsNoTracking()
                .Include(v => v.BusStops)
                .Include(v=>v.BusStopArrival)
                .Include(v=>v.BusStopDeparture)
                .Include(v=>v.Orders)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public override Task<List<Voyage>> GetAllAsync()
        {
            return _context.Voyages
                .Include(s => s.BusStops)
                .Include(v => v.BusStopArrival)
                .Include(v => v.BusStopDeparture)
                .Include(v => v.Orders)
                .ToListAsync();
        }
        public async Task<IEnumerable<Voyage>> Filter(VoyageFilter filter)
        {
            var filteredVoyages = _context.Voyages
                .Include(s => s.BusStops)
                .Include(v => v.BusStopArrival)
                .Include(v => v.BusStopDeparture)
                .AsQueryable();

            if (filter.Number.HasValue)
                filteredVoyages = filteredVoyages.Where(c => c.Number >= filter.Number.Value);

            if (!string.IsNullOrWhiteSpace(filter.NameContains))
                filteredVoyages = filteredVoyages.Where(c =>
                    c.Name.Contains(filter.NameContains));

            if (filter.DepartureFrom.HasValue)
                filteredVoyages = filteredVoyages.Where(c =>
                    c.DepartureInfo >= filter.DepartureFrom.Value);

            if (filter.ArrivalTo.HasValue)
                filteredVoyages = filteredVoyages.Where(c =>
                    c.ArrivalInfo <= filter.ArrivalTo.Value);

            if (!string.IsNullOrWhiteSpace(filter.DepartureStopNameContains))
                filteredVoyages = filteredVoyages.Where(c =>
                    c.BusStopDeparture.Name.Contains(filter.DepartureStopNameContains));

            if (!string.IsNullOrWhiteSpace(filter.ArrivalStopNameContains))
                filteredVoyages = filteredVoyages.Where(c =>
                    c.BusStopArrival.Name.Contains(filter.ArrivalStopNameContains));

            if (filter.PriceFrom.HasValue)
                filteredVoyages = filteredVoyages.Where(c => c.TicketCost >= filter.PriceFrom.Value);

            if (filter.PriceTo.HasValue)
                filteredVoyages = filteredVoyages.Where(c => c.TicketCost <= filter.PriceTo.Value);

            return await filteredVoyages.ToListAsync();
        }
    }
}
