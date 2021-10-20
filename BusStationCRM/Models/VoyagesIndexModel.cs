using System.Collections.Generic;
using BusStationCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusStationCRM.Views.Voyages
{
    public class VoyagesIndexModel : PageModel
    {
        public List<VoyageModel> Voyages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
    }
}
