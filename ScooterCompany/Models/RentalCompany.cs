using ScooterCompany.Exception;
using ScooterCompany.Interfaces;

namespace ScooterCompany.Models
{
    public class RentalCompany : IRentalCompany
    {
        public string Name { get; }
        private readonly IScooterService _scooterService;
        private readonly IList<RentedScooters> _rentedScooters;
        private readonly IRentalCalculator _calculator;

        public RentalCompany(string name,
            IScooterService service, 
            IList<RentedScooters> archive, 
            IRentalCalculator calculator)
        {
            Name = name ?? throw new InvalidCompanyNameException();
            _scooterService = service;
            _rentedScooters = archive;
            _calculator = calculator;
        }

        public void StartRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            if (scooter.IsRented)
            {
                throw new ScooterIsRentedException();
            }

            scooter.IsRented = true;
            _rentedScooters.Add(new RentedScooters
            {
                Id = scooter.Id,
                Price = scooter.PricePerMinute,
                RentStarted = DateTime.UtcNow
            });
        }

        public decimal EndRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            if (!scooter.IsRented)
            {
                throw new ScooterNotRentedException();
            }

            scooter.IsRented = false;
            var rentEndedScooter = _rentedScooters
                .First(s => s.Id == scooter.Id && !s.RentEnded.HasValue);
            rentEndedScooter.RentEnded = DateTime.UtcNow;

            return _calculator.CalculateRent(rentEndedScooter);
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new NotImplementedException();
        }
     }
}
