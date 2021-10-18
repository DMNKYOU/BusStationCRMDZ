using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusStationCRM.BLL.Enums;
using BusStationCRM.BLL.Interfaces;
using BusStationCRM.BLL.Models;
using BusStationCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BusStationCRM.Controllers
{
    public class OrdersController : Controller
    {

        private readonly ITicketsService _ticketsService;
        private readonly IOrdersService _ordersService;
        private readonly IVoyagesService _voyagesService;
        private readonly UserManager<User> userManager;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public OrdersController(IMapper mapper, IVoyagesService voyagesService, ITicketsService ticketsService,
            UserManager<User> manager, IOrdersService ordersService, ILogger<BusStopsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _ordersService = ordersService;
            _ticketsService = ticketsService;
            _voyagesService = voyagesService;
            userManager = manager;

        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var ticketsAll = await _ticketsService.GetAllAsync();
                var resList = _mapper.Map<List<Ticket>, List<TicketModel>>(ticketsAll);

                return View();//resList
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
                return StatusCode(500);
            }
        }
        // GET: /Add
        [HttpGet]
        public async Task<IActionResult> AddAsync(int? voyageId, int? idStatusOrder)
        {
            if(!(voyageId.HasValue && idStatusOrder.HasValue))
                return RedirectToAction("Index", "Voyages");
            var voyage = _mapper.Map<VoyageModel>(await _voyagesService.GetByIdAsync(voyageId.Value));
            var resModel = new OrderModel()
            {
                SeatNumber = voyage.NumberSeats, 
                Status = (Status)idStatusOrder.Value,
                VoyageId = voyageId.Value,
                Voyage = voyage
            };
            return View("Add", resModel);
        }

        // POST: /Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync(OrderModel orderModel)
        {
            try
            { 
                if(orderModel.Voyage.NumberSeats < 1)
                    return RedirectToAction("Index", "Voyages");

                orderModel.Voyage.NumberSeats -= 1;
                var voyage = _mapper.Map<Voyage>(orderModel.Voyage);
                orderModel.Voyage = null;
                orderModel.User = await userManager.GetUserAsync(User);
                await _ordersService.AddAsync(_mapper.Map<Order>(orderModel));
                await _voyagesService.EditAsync(voyage);
                await _ticketsService.AddAsync(new Ticket()
                {
                    SeatNumber = voyage.NumberSeats + 1, 
                    Status = orderModel.Status, 
                    Order = _mapper.Map<Order>(orderModel),
                    User = await userManager.GetUserAsync(User)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
                // return StatusCode(500);
            }

            return RedirectToAction("Index", "Voyages");
        }
    }
}
