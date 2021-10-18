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
        private readonly UserManager<User> _userManager;

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
            _userManager = manager;

        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var ordersAll = await _ordersService.FindAsync(c=> c.UserId == user.Id);
                var resList = _mapper.Map<List<Order>, List<OrderModel>>(await ordersAll.ToListAsync());

                return View(resList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
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

                orderModel.User = await _userManager.GetUserAsync(User);
                await _ordersService.AddOrderAndTicket(_mapper.Map<Order>(orderModel));
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
