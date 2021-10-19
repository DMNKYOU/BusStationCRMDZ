using System.Collections.Generic;
using BusStationCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusStationCRM.Views.BusStops
{
    public class Index : PageModel
    {
        public List<BusStopModel> BusStops { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
    }
}
