using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Exception
{
    public class ScooterIsRentedException : SystemException
    {
        public ScooterIsRentedException() : base("Scooter is rented and not available")
        {

        }

        public ScooterIsRentedException(string message) : base(message)
        {

        }

        public ScooterIsRentedException(string message, SystemException exception) :
            base(message, exception)
        {

        }
    }
}
