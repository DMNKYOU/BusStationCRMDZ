using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class VoyagesController : Controller
    {

        private readonly IVoyagesService _voyagesService;
        private readonly IBusStopsService _busStopsService;
        private readonly IOrdersService _ordersService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public VoyagesController(IMapper mapper, IVoyagesService voyagesService,
            IOrdersService ordService,IBusStopsService busStopsService,
            ILogger<BusStopsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _voyagesService = voyagesService;
            _busStopsService = busStopsService;
            _ordersService = ordService;
        }

        [AllowAnonymous]
        // GET
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var voyages = await _voyagesService.GetAllAsync();
                var resList = _mapper.Map<List<Voyage>, List<VoyageModel>>(voyages);

                return View(resList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
            }
        }

        //method used for add and edit
        [HttpGet]
        public async Task<IActionResult> EditAsync(int? id)
        {
            VoyageModel voyage;
            try
            {
                voyage = id.HasValue
                    ? _mapper.Map<VoyageModel>(await _voyagesService.GetByIdAsync(id.Value))
                    : new VoyageModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
            }

            ViewBag.Action = id.HasValue ? "Edit": "Add";
            ViewBag.BusStops = _mapper.Map<List<BusStop>, List<BusStopModel>>(await _busStopsService.GetAllAsync());

            return View(voyage);

        }

        // POST: BusStopsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(policy: "")]
        public async Task<ActionResult> EditAsync(VoyageModel voyageModel)
        {

            voyageModel.Name = voyageModel.Name.Trim();
            //voyageModel.TravelTime = (voyageModel.ArrivalInfo.Subtract(voyageModel.DepartureInfo)).To;
            if (!ModelState.IsValid)
            {
                ViewBag.BusStops = _mapper.Map<List<BusStop>, List<BusStopModel>>(await _busStopsService.GetAllAsync());
                return View("Edit", voyageModel);
            }

            var voyage = _mapper.Map<Voyage>(voyageModel);
            try
            {
                if (voyage.Id != 0)
                    await _voyagesService.EditAsync(voyage);
                else
                    await _voyagesService.AddAsync(voyage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
            }
            
            return RedirectToAction("Index", "Voyages");
        }


        [HttpGet]
        [Authorize]//(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _voyagesService.DeleteAsync(id);
                return RedirectToAction("Index", "Voyages");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
            }
        }

    }
}
