using System;

namespace BusStationCRM.Models
{
    public class VoyageFilterModel
    {
        public int? Number { get; set; }
        public string NameContains { get; set; }
        public string DepartureStopNameContains { get; set; }
        public string ArrivalStopNameContains { get; set; }
        public DateTime DepartureInfoFrom { get; set; }
        public DateTime ArrivalTo { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
    }
}