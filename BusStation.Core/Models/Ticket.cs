using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Enums;

namespace BusStationCRM.BLL.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int SeatNumber { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
