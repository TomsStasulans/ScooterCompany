using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Exception
{
    public class ScooterDuplicateException : System.Exception
    {
        public ScooterDuplicateException() : base("Scooter with this id exists")
        {

        }

        public ScooterDuplicateException(string message) : base(message)
        {

        }

        public ScooterDuplicateException(string message, SystemException exception) :
            base(message, exception)
        {

        }
    }
}
