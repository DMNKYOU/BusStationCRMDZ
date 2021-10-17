using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusStationCRM.BLL.Interfaces;
using BusStationCRM.BLL.Models;
using BusStationCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BusStationCRM.Controllers
{
    public class TicketsController : Controller
    {

        private readonly ITicketsService _ticketsService;
        private readonly IOrdersService _ordersService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public TicketsController(IMapper mapper, ITicketsService ticketsService,
            IOrdersService ordersService, ILogger<BusStopsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _ordersService = ordersService;
            _ticketsService = ticketsService;
        }

        public async Task<IActionResult> IndexAsync()//FIND WHERE USER=USER//FIND WHERE USER=USER//FIND WHERE USER=USER//FIND WHERE USER=USER
        {
            try
            {
                var ticketsAll = await _ticketsService.GetAllAsync();
                var resList = _mapper.Map<List<Ticket>, List<TicketModel>>(ticketsAll);

                return View(resList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
            }
        }
    }
}
