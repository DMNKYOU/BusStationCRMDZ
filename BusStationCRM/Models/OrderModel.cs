using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models;
using BusStationCRM.Enums;

namespace BusStationCRM.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public Status Status { get; set; }

        public int VoyageId { get; set; }
        public VoyageModel Voyage { get; set; }

        public TicketModel Ticket { get; set; }
        public User User { get; set; }
    }
}
