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
        private readonly ITicketsService _ticketsService;
        private readonly IVoyagesService _voyagesService;

        public OrdersService(IRepositoryAsync<Order> ordersRepository, ITicketsService ticketsService, IVoyagesService voyagesService)
        {
            _ordersRepository = ordersRepository;
            _ticketsService = ticketsService;
            _voyagesService = voyagesService;
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
        public async Task AddOrderAndTicket(Order entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            entity.Voyage.NumberSeats -= 1;
            var seatNumber = entity.Voyage.NumberSeats + 1;
            await _voyagesService.EditAsync(entity.Voyage);

            entity.Voyage = null;
            await _ticketsService.AddAsync(new Ticket()
            {
                SeatNumber = seatNumber,
                Status = entity.Status,
                Order = entity,
                User = entity.User
            });
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

        public async Task<IAsyncEnumerable<Order>> FindAsync(Func<Order, bool> predicate)
        {
            return await _ordersRepository.FindAsync(predicate);
        }
    }
}
