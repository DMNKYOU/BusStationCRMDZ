using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BusStationCRM.BLL.Extensions;
using BusStationCRM.BLL.Interfaces;
using BusStationCRM.BLL.Models;
using BusStationCRM.BLL.Models.Search;
using BusStationCRM.DAL.Interfaces;

namespace  BusStationCRM.BLL.Services
{
    public class VoyagesService : IVoyagesService
    {
        private readonly IVoyagesRepository _voyagesRepository;

        public VoyagesService(IVoyagesRepository voyageRepository)
        {
            _voyagesRepository = voyageRepository;
        }

        public async Task<List<Voyage>> GetAllAsync()
        {
            return await _voyagesRepository.GetAllAsync();
        }

        public async Task<Voyage> GetByIdAsync(int id)
        {
            return await _voyagesRepository.GetAsync(id);
        }

        public async Task AddAsync(Voyage entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            entity.TravelTime = new DateTime() + (entity.ArrivalInfo - entity.DepartureInfo);
            entity.NumberSeats = entity.NumberSeats > 0 ? entity.NumberSeats : 1;
            await _voyagesRepository.CreateAsync(entity);
        }

        public async Task EditAsync(Voyage entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            entity.TravelTime = new DateTime() + (entity.ArrivalInfo - entity.DepartureInfo);
            entity.NumberSeats = entity.NumberSeats >= 0 ? entity.NumberSeats : 0;
            await _voyagesRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _voyagesRepository.DeleteAsync(id);
        }

        public async Task<IAsyncEnumerable<Voyage>> FindAsync(Func<Voyage, bool> predicate)
        {
            return await _voyagesRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Voyage>> Search(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return await _voyagesRepository.GetAllAsync();

            search = search.NormalizeSearchString();
            var resault = await _voyagesRepository.FindAsync(c =>
                c.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                c.BusStopDeparture.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                c.BusStopArrival.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

            return await resault.ToListAsync();
        }

        public async Task<IEnumerable<Voyage>> Filter(VoyageFilter filter)
        {
            return await _voyagesRepository.Filter(filter);
        }
    }
}
