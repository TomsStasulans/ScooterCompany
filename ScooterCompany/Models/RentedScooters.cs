using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Models
{
    public class RentedScooters
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public DateTime RentStarted { get; set; }
        public DateTime? RentEnded { get; set; }
    }
}
