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
using BusStationCRM.Views.BusStops;

namespace BusStationCRM.Controllers
{
    public class BusStopsController : Controller
    {

        private readonly IBusStopsService _busStopsService;
        private readonly IVoyagesService _voyagesService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public BusStopsController(IMapper mapper, IBusStopsService busStopsService,
            IVoyagesService voyagesService, ILogger<BusStopsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _busStopsService = busStopsService;
            _voyagesService = voyagesService;
        }

        [AllowAnonymous]
        // GET: BusStopsController
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var stops = await _busStopsService.GetAllAsync();
                var resList = _mapper.Map<List<BusStop>, List<BusStopModel>>(stops);
                return View(new Views.BusStops.BusStopsIndexModel()
                {
                    BusStops = resList,
                    SearchTerm = null
                });
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
            BusStopModel stopModel;
            try
            {
                stopModel = id.HasValue
                    ? _mapper.Map<BusStopModel>(await _busStopsService.GetByIdAsync(id.Value))
                    : new BusStopModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
            }

            ViewBag.Action = id.HasValue ? "Edit": "Add";

            return View(stopModel);

        }

        // POST: BusStopsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(policy: "")]
        public async Task<ActionResult> EditAsync(BusStopModel stopModel)
        {

            stopModel.Name = stopModel.Name.Trim();
            stopModel.Description = stopModel.Description.Trim();

            if (!ModelState.IsValid)
                return View("Edit", stopModel);

            var stop = _mapper.Map<BusStop>(stopModel);
            try
            {
                if (stop.Id != 0)
                    await _busStopsService.EditAsync(stop);
                else
                    await _busStopsService.AddAsync(stop);

                return RedirectToAction("Index", "BusStops");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
            }
        }


        [HttpGet]
        [Authorize]//(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _busStopsService.DeleteAsync(id);
                return RedirectToAction("Index", "BusStops");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Error", new { @statusCode = 500 });
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> SearchAsync(string search)
        {
            var courses = await _busStopsService.Search(search);

            return View("Index", new Views.BusStops.BusStopsIndexModel()
            {
                BusStops = _mapper.Map<IEnumerable<BusStop>, List<BusStopModel>>(courses),
                SearchTerm = search
                
            });
        }
    }
}
