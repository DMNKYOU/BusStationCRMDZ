using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusStationCRM.BLL.Interfaces;
using BusStationCRM.BLL.Models;
using BusStationCRM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BusStationCRM.Controllers
{
    public class TicketsController : Controller
    {

        private readonly ITicketsService _ticketsService;
        private readonly IOrdersService _ordersService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly UserManager<User> _userManager;

        public TicketsController(IMapper mapper, ITicketsService ticketsService,
            IOrdersService ordersService, ILogger<BusStopsController> logger, UserManager<User> userManager)
        {
            _mapper = mapper;
            _logger = logger;
            _ordersService = ordersService;
            _ticketsService = ticketsService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var ticketsAll = await _ticketsService.FindAsync(c => c.UserId == user.Id);
                var resList = _mapper.Map<List<Ticket>, List<TicketModel>>(await ticketsAll.ToListAsync());

                return View(resList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> BuyReservedTicket(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var ticket = await _ticketsService.GetByIdAsync(id);

                if (ticket!= null && ticket.UserId != user.Id)
                    return RedirectToAction("Error", "Error", new { @statusCode = 403 });
                
                await _ticketsService.UpdateStatusTicketOrder(ticket);

                return RedirectToAction("Index", "Tickets");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
            }
        }
    }
}
