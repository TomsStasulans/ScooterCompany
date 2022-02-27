namespace ScooterCompany.Exception
{
    public class ScooterDoesNotExistException : SystemException
    {
        public ScooterDoesNotExistException() : base("Scooter with this id does not exist")
        {

        }

        public ScooterDoesNotExistException(string message) : base(message)
        {

        }

        public ScooterDoesNotExistException(string message, System.Exception exception) :
            base(message, exception)
        {

        }
    }
}
