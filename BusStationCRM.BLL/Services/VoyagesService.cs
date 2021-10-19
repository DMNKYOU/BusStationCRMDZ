using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusStationCRM.BLL.Extensions;
using BusStationCRM.BLL.Interfaces;
using BusStationCRM.BLL.Models;
using BusStationCRM.DAL.Interfaces;

namespace  BusStationCRM.BLL.Services
{
    public class VoyagesService : IVoyagesService
    {
        private readonly IRepositoryAsync<Voyage> _voyagesService;

        public VoyagesService(IRepositoryAsync<Voyage> voyageRepository)
        {
            _voyagesService = voyageRepository;
        }

        public async Task<List<Voyage>> GetAllAsync()
        {
            return await _voyagesService.GetAllAsync();
        }

        public async Task<Voyage> GetByIdAsync(int id)
        {
            return await _voyagesService.GetAsync(id);
        }

        public async Task AddAsync(Voyage entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            entity.TravelTime = new DateTime() + (entity.ArrivalInfo - entity.DepartureInfo);
            entity.NumberSeats = entity.NumberSeats > 0 ? entity.NumberSeats : 1;
            await _voyagesService.CreateAsync(entity);
        }

        public async Task EditAsync(Voyage entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            entity.TravelTime = new DateTime() + (entity.ArrivalInfo - entity.DepartureInfo);
            entity.NumberSeats = entity.NumberSeats >= 0 ? entity.NumberSeats : 0;
            await _voyagesService.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _voyagesService.DeleteAsync(id);
        }

        public async Task<IAsyncEnumerable<Voyage>> FindAsync(Func<Voyage, bool> predicate)
        {
            return await _voyagesService.FindAsync(predicate);
        }

        public async Task<IEnumerable<Voyage>> Search(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return await _voyagesService.GetAllAsync();

            search = search.NormalizeSearchString();
            var resault = await _voyagesService.FindAsync(c =>
                c.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                c.BusStopDeparture.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                c.BusStopArrival.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

            return await resault.ToListAsync();
        }
    }
}
