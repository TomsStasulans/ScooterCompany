using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ScooterCompany.Exception;
using ScooterCompany.Interfaces;
using ScooterCompany.Models;

namespace ScooterCompany.Tests
{
    public class RentalCompanyTests
    {
        private IRentalCompany _target;
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
            _target = new RentalCompany(_defaultName, _scooterService, _rentedScooters, _calculator);
            _scooterService.AddScooter(_id, _defaultPrice);
        }

        [Test]
        public void CreateCompany_company_ShouldBeEqual()
        {
            //Assert
            Assert.AreEqual(_defaultName, _target.Name);
        }

        [Test]
        public void CreateCompany_company_ShouldBeNull()
        {
            //Assert
            Assert.Throws<InvalidCompanyNameException>(() => _target = new RentalCompany(null, _scooterService, _rentedScooters, _calculator));
        }

        [Test]
        public void StartRent_RentFirstScooter_ShouldStartRent()
        {
            //Act
            _target.StartRent(_id);

            //Assert
            Assert.IsTrue(_scooterService.GetScooterById(_id).IsRented);
        }

        [Test]
        public void StartRent_RentNonExistingScooter_ShouldFail()
        {
            //Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.StartRent("2"));
        }

        [Test]
        public void StartRent_RentRentedScooter_ShouldThrowScooterIsRentedException()
        {
            //Arrange
            _target.StartRent(_id);

            //Assert
            Assert.Throws<ScooterIsRentedException>(() => _target.StartRent(_id));
        }

        [Test]
        public void StartRent_ScooterRented_RentedListShouldBeUpdated()
        {
            //Act
            _target.StartRent(_id);

            //Assert
            var rentedScooter = _rentedScooters.First(s => s.Id == _id);
            Assert.AreEqual(1, _rentedScooters.Count);
            Assert.AreEqual(_defaultPrice, rentedScooter.Price);
        }

        [Test]
        public void EndRent_EndRent_ShouldReturnMoreThanZerro()
        {
            //Arrange
            _target.StartRent(_id);

            //Act
            var result = _target.EndRent(_id);

            //Assert
            Assert.GreaterOrEqual(result, 0);
        }

        [Test]
        public void EndRent_EndRentNotExistingScooter_ShouldThrowException()
        {
            //Assert
            Assert.Throws<ScooterNotRentedException>(() => _target.EndRent(_id));
        }

        [Test]
        public void EndRent_ScooterRentEnded_RentedListShouldBeUpdated()
        {
            //Arrange
            _target.StartRent(_id);

            //Act
            _target.EndRent(_id);

            //Assert
            var rentedScooter = _rentedScooters.First(s => s.Id == _id);
            Assert.AreEqual(1, _rentedScooters.Count);
            Assert.NotNull(rentedScooter.RentEnded);
        }

        [Test]
        public void EndRent_ScooterRented1Day_ShouldReturn20()
        {
            //Arrange
            var id = "2";
            var startRent = DateTime.UtcNow.AddMinutes(-100);
            _rentedScooters.Add(new RentedScooters
            {
                Id = id,
                RentStarted = startRent,
                Price = _defaultPrice
            });
            _scooterService.AddScooter(id, _defaultPrice);
            _scooterService.GetScooterById(id).IsRented = true;

            //Act
            var result = _target.EndRent(id);

            //Assert
            Assert.AreEqual(20.0m, result);
        }

        [Test]
        public void EndRent_ScooterRented3hours_ShouldReturn20()
        {
            //Arrange
            var id = "2";
            var startRent = DateTime.UtcNow.AddMinutes(-180);
            _rentedScooters.Add(new RentedScooters
            {
                Id = id,
                RentStarted = startRent,
                Price = _defaultPrice
            });
            _scooterService.AddScooter(id, _defaultPrice);
            _scooterService.GetScooterById(id).IsRented = true;

            //Act
            var result = _target.EndRent(id);

            //Assert
            Assert.AreEqual(20.0m, result);
        }
    }
}
