using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Enums;

namespace BusStationCRM.BLL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public Status Status { get; set; }

        public int VoyageId { get; set; }
        public Voyage Voyage { get; set; }

        public Ticket Ticket { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
