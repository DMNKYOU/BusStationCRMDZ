﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models;
using BusStationCRM.Enums;

namespace BusStationCRM.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int SeatNumber { get; set; }

        public int OrderId { get; set; }
        public OrderModel Order { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
