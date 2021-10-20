using System;
using System.ComponentModel.DataAnnotations;

namespace BusStationCRM.Models
{
    public class VoyageFilterModel
    {
        [Range(0, int.MaxValue)]
        public int? Number { get; set; }
        public string NameContains { get; set; }
        public string DepartureStopNameContains { get; set; }
        public string ArrivalStopNameContains { get; set; }
        public DateTime? DepartureFrom { get; set; }
        public DateTime? ArrivalTo { get; set; }

        [RegularExpression(@"^[+]?[0-9]*\.?[0-9]+$", ErrorMessage = "This field can be contain only numbers")]
        public double? PriceFrom { get; set; }

        [RegularExpression(@"^[+]?[0-9]*\.?[0-9]+$", ErrorMessage = "This field can be contain only numbers")]
        public double? PriceTo { get; set; }
    }
}