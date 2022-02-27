using ScooterCompany.Models;

namespace ScooterCompany.Interfaces
{
    public interface IRentalCalculator
    {
        decimal CalculateRent(RentedScooters scooter);
    }
}
