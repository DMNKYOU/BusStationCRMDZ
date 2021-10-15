using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BusStationCRM.BLL.Models
{
    public class User: IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }

        [PersonalData]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
