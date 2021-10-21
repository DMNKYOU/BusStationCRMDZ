using System;

namespace BusStationCRM.BLL.Models.Search
{
    public class VoyageFilter
    {
        public int? Number { get; set; }
        public string NameContains { get; set; }
        public DateTime? DepartureFrom { get; set; }
        public DateTime? ArrivalTo { get; set; }
        public string DepartureStopNameContains { get; set; }
        public string ArrivalStopNameContains { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
    }
}