using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CarConnectCSharp.dao;
using CarConnectCSharp.Entity;

namespace CarTest
{
    class VehiclesTest
    {
        private VehicleService _vehicleService;

        [SetUp]
        public void Setup()
        {
            _vehicleService = new VehicleService();
        }

        [Test]
        public void AddVehicle_ValidVehicle_ReturnsTrue()
        {
            // Arrange
            var newVehicle = new Vehicle
            {
                VehicleID = 29,
                Model = "Model X",
                Make = "Tesla",
                Year = 2022,
                Color = "Red",
                RegistrationNumber = "REG" + Guid.NewGuid().ToString().Substring(0, 5),
                Availability = true,
                DailyRate = 100
            };

            // Act
            bool result = _vehicleService.AddVehicle(newVehicle);

            // Assert
            Assert.That(result, Is.True, "The vehicle should be added successfully.");
        }
        [Test]
        public void UpdateVehicle_ValidVehicleDetails_ReturnsTrue()
        {
            // Arrange
            var existingVehicle = new Vehicle
            {
                VehicleID = 3,
                Model = "Model Y",
                Make = "BMW",
                Year = 2022,
                Color = "Red",
                RegistrationNumber = "ABC1234",
                Availability = true,
                DailyRate = 100
            };

            existingVehicle.Color = "Blue";  // Update color for example

            // Act
            bool result = _vehicleService.UpdateVehicle(existingVehicle);

            // Assert
            Assert.That(result, Is.True, "The vehicle details should be updated successfully.");
        }
        [Test]
        public void GetAvailableVehicles_ReturnsAvailableVehicles()
        {
            // Arrange (optionally mock database or data)

            // Act
            var availableVehicles = _vehicleService.GetAvailableVehicles();

            // Assert
            Assert.That(availableVehicles, Is.Not.Empty, "The list of available vehicles should not be empty.");
        }
        [Test]
        public void Test_GetAllVehicles_ReturnsAll()
        {
            // Arrange
            var service = new VehicleService();

            // Act
            List<Vehicle> allVehicles = service.GetAllVehicles();

            // Assert
            Assert.That(allVehicles,Is.Not.Null);
            Assert.That(allVehicles.Count > 0); // Or your expected count
        }
    }
}