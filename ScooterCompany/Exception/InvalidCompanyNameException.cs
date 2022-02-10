using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterCompany.Exception
{
    public class InvalidCompanyNameException : System.Exception
    {
        public InvalidCompanyNameException() : base("Company name is required")
        {

        }

        public InvalidCompanyNameException(string message) : base(message)
        {

        }

        public InvalidCompanyNameException(string message, SystemException exception) :
            base(message, exception)
        {

        }
    }
}
