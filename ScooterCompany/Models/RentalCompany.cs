using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScooterCompany.Exception;
using ScooterCompany.Interfaces;

namespace ScooterCompany.Models
{
    public class RentalCompany : IRentalCompany
    {
        public string Name { get; }
        private readonly IScooterService _scooterService;

        public RentalCompany(string name, IScooterService service)
        {
            Name = name ?? throw new InvalidCompanyNameException();
            service = _scooterService;
        }

        public void StartRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            scooter.IsRented = true;
        }

        public decimal EndRent(string id)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new NotImplementedException();
        }
    }
}
