using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Enums;
using BusStationCRM.BLL.Models;

namespace BusStationCRM.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int SeatNumber { get; set; }

        public int OrderId { get; set; }
        public OrderModel Order { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
