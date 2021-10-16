using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
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

            await _voyagesService.CreateAsync(entity);
        }

        public async Task EditAsync(Voyage entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            await _voyagesService.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _voyagesService.DeleteAsync(id);
        } 

    }
}
