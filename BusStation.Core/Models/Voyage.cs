using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using BusStationCRM.BLL.Enums;

namespace BusStationCRM.BLL.Models
{
    public class Voyage
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime DepartureInfo { get; set; }
        public DateTime ArrivalInfo { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime TravelTime { get; set; }

        public int NumberSeats { get; set; }
        public double TicketCost { get; set; }

        public TypeTransport Type { get; set; }


        [ForeignKey("BusStopDeparture")]
        public int BusStopDepartureId { get; set; }
        public BusStop BusStopDeparture { get; set; }


        [ForeignKey("BusStopArrival")]
        public int BusStopArrivalId { get; set; }
        public BusStop BusStopArrival { get; set; }

        public ICollection<BusStop> BusStops { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}