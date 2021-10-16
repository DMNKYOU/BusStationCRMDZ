using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using BusStationCRM.BLL.Enums;

namespace BusStationCRM.Models
{
    public class VoyageModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime DepartureInfo { get; set; }
        public DateTime ArrivalInfo { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true)]///////////////ADD ATTR OTPR-PRIB
        public DateTime TravelTime { get; set; }

        public int NumberSeats { get; set; }
        public int TicketCost { get; set; }

        public TypeTransport Type { get; set; }


        [ForeignKey("BusStopDeparture")]
        public int BusStopDepartureId { get; set; }
        public BusStopModel BusStopDeparture { get; set; }


        [ForeignKey("BusStopArrival")]
        public int BusStopArrivalId { get; set; }
        public BusStopModel BusStopArrival { get; set; }

        public ICollection<BusStopModel> BusStops { get; set; }
        public ICollection<OrderModel> Orders { get; set; }
    }
}