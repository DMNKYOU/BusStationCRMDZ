using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusStationCRM.BLL.Enums;
using BusStationCRM.BLL.Interfaces;
using BusStationCRM.BLL.Models;
using BusStationCRM.DAL.Interfaces;

namespace  BusStationCRM.BLL.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly IRepositoryAsync<Ticket> _ticketsRepository;
        private readonly IRepositoryAsync<Order> _ordersWorkAsync;

        public TicketsService(IRepositoryAsync<Ticket> ticketRepository, IRepositoryAsync<Order> ordersWorkAsync)
        {
            _ticketsRepository= ticketRepository;
            _ordersWorkAsync = ordersWorkAsync;
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _ticketsRepository.GetAllAsync();
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await _ticketsRepository.GetAsync(id);
        }

        public async Task AddAsync(Ticket entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            await _ticketsRepository.CreateAsync(entity);
        }

        public async Task EditAsync(Ticket entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            await _ticketsRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _ticketsRepository.DeleteAsync(id);
        }

        public async Task<IAsyncEnumerable<Ticket>> FindAsync(Func<Ticket, bool> predicate)
        {
            return await _ticketsRepository.FindAsync(predicate);
        }

        public async Task UpdateStatusTicketOrder(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException();

            ticket.Status = Status.BoughtOut;
            ticket.Order.Status = Status.BoughtOut;

            await EditAsync(ticket);
            await _ordersWorkAsync.UpdateAsync(ticket.Order);
        }
    }
}
