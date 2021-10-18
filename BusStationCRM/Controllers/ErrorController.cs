using BusStationCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BusStationCRM.Models;
namespace BusStationCRM.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            var error = new ErrorViewModel() { StatusCode = statusCode };
            switch (statusCode)
            {
                case 401:
                    error.Message = "Access to this resource is denied (auth error)";
                    break;
                case 403:
                    error.Message = "Accessing operation or resource you were trying to reach is absolutely forbidden for you";
                    break;
                case 404:
                    error.Message = "Sorry, that page doesn't exist.";
                    break;
                case 500:
                    error.Message = "Internal Server Error.";
                    break;
            }

            return View("Error", error);
        }
    }
}
