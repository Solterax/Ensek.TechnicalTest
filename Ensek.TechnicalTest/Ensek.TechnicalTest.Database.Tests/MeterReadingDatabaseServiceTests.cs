using Ensek.TechnicalTest.Database.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ensek.TechnicalTest.Database.Tests
{
    public class MeterReadingDatabaseServiceTests
    {
        private MeterReadingDatabaseService _meterReadingDatabaseService;
        private Mock<IMeterReadingDataAccess> _mockMeterReadingDataAccess;

        [SetUp]
        public void Setup()
        {
            _mockMeterReadingDataAccess = new Mock<IMeterReadingDataAccess>();
            _meterReadingDatabaseService = new MeterReadingDatabaseService(_mockMeterReadingDataAccess.Object);
        }

        [Test]
        public async Task GivenANewListOfMeterReadings_WhenSaveMeterREadingsAsyncExecutes_TheDatabaseModelIsPassedToTheDataAccess()
        {
            var inputMeterReadings = new List<Domain.Models.MeterReading>()
            {
                new Domain.Models.MeterReading()
                {
                    AccountId = 1,
                    MeterReadingDateTime = DateTime.UtcNow,
                    MeterReadValue = 10
                }
            };

            await _meterReadingDatabaseService.SaveMeterReadingsAsync(inputMeterReadings);

            _mockMeterReadingDataAccess.Verify(x => x.SaveMeterReadingsAsync(It.IsAny<List<Models.MeterReading>>()));
        }
    }
}