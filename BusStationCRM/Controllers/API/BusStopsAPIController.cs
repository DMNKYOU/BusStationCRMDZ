using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusStationCRM.BLL.Interfaces;
using BusStationCRM.BLL.Models;
using BusStationCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BusStationCRM.Controllers.API
{
    [Route("/api/[controller]")]
    [ApiController]
    public class BusStopsAPIController : ControllerBase
    {
        private readonly IBusStopsService _busStopsService;
        private readonly IVoyagesService _voyagesService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public BusStopsAPIController(IMapper mapper, IBusStopsService busStopsService,
            IVoyagesService voyagesService, ILogger<BusStopsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _busStopsService = busStopsService;
            _voyagesService = voyagesService;
        }

        [AllowAnonymous]
        // GET: BusStopsController
        public async Task<IEnumerable<BusStopModel>> GetBusStops()
        {
            return _mapper.Map<List<BusStopModel>>(await _busStopsService.GetAllAsync());
        }
    }
}