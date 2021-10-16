using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Enums;

namespace BusStationCRM.Models
{
    public class BusStopModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Transport's type")]
        public TypeTransport Type { get; set; }

        [InverseProperty("BusStopArrival")]
        public List<VoyageModel> ArrivalVoyages { get; set; } = new List<VoyageModel>();

        [InverseProperty("BusStopDeparture")]
        public List<VoyageModel> DepartureVoyages { get; set; } = new List<VoyageModel>();

        public List<VoyageModel> Voyages { get; set; } = new List<VoyageModel>();
    }
}
