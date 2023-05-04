using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ScooterCompany.Interfaces;
using ScooterCompany.Models;

namespace ScooterCompany.Tests
{
    public class RentalCalculatorTests
    {
        private IRentalCompany _company;
        private IScooterService _scooterService;
        private IList<RentedScooters> _rentedScooters;
        private IRentalCalculator _calculator;
        private string _defaultName = "Company";
        private string _id = "1";
        private decimal _defaultPrice = 0.20m;

        [SetUp]
        public void Setup()
        {
            _scooterService = new ScooterService();
            _rentedScooters = new List<RentedScooters>();
            _calculator = new RentalCalculator();
            _company = new RentalCompany(_defaultName, _scooterService, _rentedScooters, _calculator);
            _scooterService.AddScooter(_id, _defaultPrice);
        }


        [Test]
        public void EndRent_ScooterRent_ShouldReturn30()
        {
            //Arrange
            var id = "2";
            var startRent = DateTime.UtcNow.AddMinutes(-47);
            _scooterService.AddScooter(id, _defaultPrice);
            _scooterService.GetScooterById(id).IsRented = true;

            _rentedScooters.Add(new RentedScooters
            {
                Id = id,
                RentStarted = startRent,
                Price = _defaultPrice
            });

            //Act
            var result = _company.EndRent(id);

            //Assert
            Assert.AreEqual(30.0m, result);
        }
    }
}
