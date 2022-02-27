using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Exception
{
    public class ScooterNotFoundException : SystemException
    {
        public ScooterNotFoundException() : base("Scooter is not rented")
        {

        }

        public ScooterNotFoundException(string message) : base(message)
        {

        }

        public ScooterNotFoundException(string message, System.Exception exception) :
            base(message, exception)
        {

        }
    }
}
