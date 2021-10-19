using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BusStationCRM.BLL.Extensions;
using BusStationCRM.BLL.Interfaces;
using BusStationCRM.BLL.Models;
using BusStationCRM.DAL.Interfaces;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace  BusStationCRM.BLL.Services
{
    public class BusStopsService : IBusStopsService
    {
        private readonly IRepositoryAsync<BusStop> _busStopsRepository;

        public BusStopsService(IRepositoryAsync<BusStop> stopsRepository)
        {
            _busStopsRepository = stopsRepository;
        }

        public async Task<List<BusStop>> GetAllAsync()
        {
            return await _busStopsRepository.GetAllAsync();
        }

        public async Task<BusStop> GetByIdAsync(int id)
        {
            return await _busStopsRepository.GetAsync(id);
        }

        public async Task AddAsync(BusStop entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            await _busStopsRepository.CreateAsync(entity);
        }

        public async Task EditAsync(BusStop entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            await _busStopsRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _busStopsRepository.DeleteAsync(id);
        }

        public async Task<IAsyncEnumerable<BusStop>> FindAsync(Func<BusStop, bool> predicate)
        {
            return await _busStopsRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<BusStop>> Search(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return await _busStopsRepository.GetAllAsync();

            search = search.NormalizeSearchString();
            var resault = await _busStopsRepository.FindAsync(c =>
                c.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                c.Description.Contains(search, StringComparison.OrdinalIgnoreCase));

            return await resault.ToListAsync();
        }
    }
}
