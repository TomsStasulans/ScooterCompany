using NUnit.Framework;
using ScooterCompany.Exception;
using ScooterCompany.Interfaces;
using ScooterCompany.Models;

namespace ScooterCompany.Tests
{
    public class RentalCompanyTests
    {
        private IRentalCompany _target;
        private IScooterService _service;
        private string _defaultName = "Company";
        [SetUp]
        public void Setup()
        {
            _target = new RentalCompany(_defaultName, _service);
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
            Assert.Throws<InvalidCompanyNameException>(() => _target = new RentalCompany(null, _service));
        }

        [Test]
        public void StartRent_RentFirstScooter_ShouldStartRent()
        {
            //Arrange
            var id = "1";
            var service = new ScooterService();
            service.AddScooter(id, 0.20m);
            //Act
            _target.StartRent(id);

            //Assert
            Assert.IsTrue(service.GetScooterById(id).IsRented);
        }
    }
}
