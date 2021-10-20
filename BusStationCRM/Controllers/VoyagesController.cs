using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusStationCRM.BLL.Interfaces;
using BusStationCRM.BLL.Models;
using BusStationCRM.BLL.Models.Search;
using BusStationCRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BusStationCRM.Controllers
{
    public class VoyagesController : Controller
    {

        private readonly IVoyagesService _voyagesService;
        private readonly IBusStopsService _busStopsService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public VoyagesController(IMapper mapper, IVoyagesService voyagesService,
            IBusStopsService busStopsService,
            ILogger<BusStopsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _voyagesService = voyagesService;
            _busStopsService = busStopsService;
        }

        [AllowAnonymous]
        // GET
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var voyages = await _voyagesService.GetAllAsync();
                var resList = _mapper.Map<List<Voyage>, List<VoyageModel>>(voyages);

                return View( new Views.Voyages.VoyagesIndexModel() { Voyages = resList });
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

        [AllowAnonymous]
        public async Task<IActionResult> SearchAsync(string search)
        {
            var voyages = await _voyagesService.Search(search);

            return View("Index", new Views.Voyages.VoyagesIndexModel()
            {
                Voyages = _mapper.Map<IEnumerable<Voyage>, List<VoyageModel>>(voyages),
                SearchTerm = search
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Filter()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async System.Threading.Tasks.Task<IActionResult> FilterAsync(VoyageFilterModel model)
        {
            var voyagesFiltered = await _voyagesService.Filter(_mapper.Map<VoyageFilter>(model));

            return View("Index", new Views.Voyages.VoyagesIndexModel() { Voyages = _mapper.Map<IEnumerable<Voyage>, List<VoyageModel>>(voyagesFiltered) });
        }
    }
}
