using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Enums;

namespace BusStationCRM.BLL.Models
{
    public class BusStop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TypeTransport Type { get; set; }

        [InverseProperty("BusStopArrival")]
        public List<Voyage> ArrivalVoyages { get; set; } = new List<Voyage>();

        [InverseProperty("BusStopDeparture")]
        public List<Voyage> DepartureVoyages { get; set; } = new List<Voyage>();

        public List<Voyage> Voyages { get; set; } = new List<Voyage>();
    }
}
