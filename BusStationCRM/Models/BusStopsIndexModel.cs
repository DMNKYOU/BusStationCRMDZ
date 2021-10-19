using System.Collections.Generic;
using BusStationCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusStationCRM.Views.BusStops
{
    public class BusStopsIndexModel : PageModel
    {
        public List<BusStopModel> BusStops { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
    }
}
