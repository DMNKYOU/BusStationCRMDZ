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
    public class BusStopsController : Controller
    {

        private readonly IBusStopsService _busStopsService;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        public BusStopsController(IMapper mapper, IBusStopsService busStopsService, ILogger<BusStopsController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _busStopsService = busStopsService;
        }

        [AllowAnonymous]
        // GET: BusStopsController
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var stops = await _busStopsService.GetAllAsync();
                var resList = _mapper.Map<List<BusStop>, List<BusStopModel>>(stops);

                return View(resList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        // GET: BusStopsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusStopsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BusStopsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BusStopsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
                return StatusCode(500);
            }
        }
    }
}
