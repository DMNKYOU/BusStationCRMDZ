using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using BusStationCRM.BLL.Enums;
using BusStationCRM.Validation;

namespace BusStationCRM.Models
{
    public class VoyageModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Number { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "This field can be contain only letters")]
        [Display(Name = "Name", Prompt = "Name")]
        public string Name { get; set; }

        [Display(Name = "Departure time")] 
        [DateNotInThePast(ErrorMessage = "Should de not in the past")]
        public DateTime DepartureInfo { get; set; }

        [Display(Name = "Arrival time")]
        [DateCorrectRange(ErrorMessage = "Should de more then date departure")]
        public DateTime ArrivalInfo { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime TravelTime { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Available seats")]
        public int NumberSeats { get; set; }

        [Required]
        [RegularExpression(@"^[+]?[0-9]*\.?[0-9]+$", ErrorMessage = "This field can be contain only numbers")]
        [Display(Name = "Ticket price")]
        public double TicketCost { get; set; }

        public TypeTransport Type { get; set; }

        [Display(Name = "Departure stop")]
        [ForeignKey("BusStopDeparture")]
        public int BusStopDepartureId { get; set; }
        public BusStopModel BusStopDeparture { get; set; }

        [Display(Name = "Arrival stop")]
        [ForeignKey("BusStopArrival")]
        public int BusStopArrivalId { get; set; }
        public BusStopModel BusStopArrival { get; set; }

        public ICollection<BusStopModel> BusStops { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
    }
}