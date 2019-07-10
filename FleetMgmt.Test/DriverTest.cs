using System;
using System.Threading.Tasks;
using FleetMgmt.Data;
using FleetMgmt.Data.Entities;
using FleetMgmt.Repository.Implementations;
using FleetMgmt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FleetMgmt.Test
{
    public class DriverTest
    {
        private readonly IDriverRepository _driverRepository;

        public DriverTest()
        {
            var dbOptions = new DbContextOptionsBuilder<FmDbContext>()
                .UseInMemoryDatabase(databaseName: "FleetMgmt")
                .Options;
            var context = new FmDbContext(dbOptions);
            _driverRepository = new DriverRepository(context);
        }

        [Fact]
        public async void CreateDriver()
        {
            var driverId = new Guid();
            var retVal = await AddSampleDriver(driverId);

            Assert.Equal(1,retVal);
        }

        [Fact]
        public async void GetAllDrivers()
        {
            AddDriverCollection();

            var drivers = await _driverRepository.GetAllDrivers();

            Assert.Equal(10, drivers.Count);
        }

        [Fact]
        public async void GetDriver()
        {
            var driverId = new Guid("83034cec-4785-4327-befe-b78b50f464bf");
            _ = await AddSampleDriver(driverId);

            var driver = await _driverRepository.GetDriverById(driverId);

            Assert.Equal("John", driver.FirstName);
        }

        private async Task<int> AddSampleDriver(Guid id)
        {
            var driver = new Driver{ 
                Id=id,
                FirstName = "John",
                LastName = "Doe",
                IsActive = true,
                LicenseNumber = "abcd1234",
                DateOfBirth = new DateTime(1990, 01, 16)
            };
            return await _driverRepository.AddDriver(driver);
        }

        private async void AddDriverCollection()
        {
            for (int i = 0; i < 10; i++)
            {
                var driver = new Driver{ 
                    Id=Guid.NewGuid(),
                    FirstName = "John " + i,
                    LastName = "Doe " + i,
                    IsActive = true,
                    LicenseNumber = DateTime.Now.Ticks.ToString(),
                    DateOfBirth = new DateTime(1990, 01, 16)
                };
                await _driverRepository.AddDriver(driver);
            }
        }
    }
}
