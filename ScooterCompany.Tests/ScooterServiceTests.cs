using System;
using System.ComponentModel.Design;
using NUnit.Framework;
using ScooterCompany.Exception;
using ScooterCompany.Interfaces;
using ScooterCompany.Models;

namespace ScooterCompany.Tests
{
    public class ScooterServiseTests
    {
        private IScooterService _target;
        [SetUp]
        public void Setup()
        {
            _target = new ScooterService();
        }

        [Test]
        public void AddScooter_1_020_ShouldHaveOneScooter()
        {
            //Act
            _target.AddScooter("1", 0.20m);

            //Assert
            Assert.AreEqual(1, _target.GetScooters().Count);
        }

        [Test]
        public void AddScooter_Empty020_ShouldFail()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => _target.AddScooter(null, 0.20m));
        }

        [Test]
        public void AddScooter_ExistingId_ShouldFail()
        {
            //Arrange
            var id = "1";
            _target.AddScooter(id, 0.20m);
            //Assert
            Assert.Throws<ScooterDuplicateException>(() => _target.AddScooter(id, 0.20m));
        }

        [Test]
        public void AddScooter_1_negative020_ShouldFail()
        {
            //Assert
            Assert.Throws<InvalidPriceException>(() => _target.AddScooter("1", -0.20m));
        }

        [Test]
        public void RemoveScooter_1_020_ShouldRemoveScooter()
        {
            //Arrange
            var id = "1";
            var price = 0.20m;
            _target.AddScooter(id, price);
            //Act
            _target.RemoveScooter("1");

            //Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.GetScooterById(id));
        }

        [Test]
        public void RemoveScooter_1_ShouldFail()
        {
            //Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.RemoveScooter("1"));
        }

        [Test]
        public void GetScooters_ChangeInventoryWithoutService_ShouldFail()
        {
            //Arrange
            var scooters = _target.GetScooters();
            Assert.AreEqual(0, scooters.Count);

            //Act
            scooters.Add(new Scooter("1", 0.20m));
            scooters.Add(new Scooter("2", 0.20m));
            scooters.Add(new Scooter("3", 0.20m));

            //Assert
            Assert.AreEqual(0, _target.GetScooters().Count);
        }

        [Test]
        public void GetScooterById_1_ShouldReturnScooterWithId1()
        {
            //Arrange
            var id = "1";
            var price = 0.20m;
            _target.AddScooter(id, price);

            //Act
            var scooterReturned = _target.GetScooterById(id);

            //Assert
            Assert.AreEqual("1", scooterReturned.Id);
        }
    }
}