using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Exception
{
    public class ScooterNotRentedException : System.Exception
    {
        public ScooterNotRentedException() : base("Scooter is not rented")
        {

        }

        public ScooterNotRentedException(string message) : base(message)
        {

        }

        public ScooterNotRentedException(string message, SystemException exception) :
            base(message, exception)
        {

        }
    }
}
