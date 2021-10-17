using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BusStationCRM.BLL.Interfaces;
using BusStationCRM.BLL.Models;
using BusStationCRM.DAL.Interfaces;

namespace  BusStationCRM.BLL.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IRepositoryAsync<Order> _ordersRepository;

        public OrdersService(IRepositoryAsync<Order> ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _ordersRepository.GetAllAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _ordersRepository.GetAsync(id);
        }

        public async Task AddAsync(Order entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            await _ordersRepository.CreateAsync(entity);
        }

        public async Task EditAsync(Order entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            await _ordersRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _ordersRepository.DeleteAsync(id);
        } 

    }
}
