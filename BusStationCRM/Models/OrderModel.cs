using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Enums;
using BusStationCRM.BLL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusStationCRM.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[-+]?[0-9]*\.?[0-9]+$", ErrorMessage = "This field can be contain only numbers")]
        public int SeatNumber { get; set; }
        public Status Status { get; set; }

        public int VoyageId { get; set; }
        public VoyageModel Voyage { get; set; }

        public TicketModel Ticket { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
