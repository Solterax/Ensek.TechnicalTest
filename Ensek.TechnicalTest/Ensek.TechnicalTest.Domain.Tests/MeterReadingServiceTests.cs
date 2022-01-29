using Ensek.TechnicalTest.Domain.Interfaces;
using Ensek.TechnicalTest.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ensek.TechnicalTest.Domain.Tests
{
    public class MeterReadingServiceTests
    {
        private MeterReadingService _meterReadingService;
        private Mock<ICsvParser> _mockCsvParser;
        private Mock<IMeterReadingValidator> _mockMeterReadingValidator;
        private Mock<IMeterReadingDatabaseService> _mockMeterReadingDatabaseService;

        [SetUp]
        public void Setup()
        {
            _mockCsvParser = new Mock<ICsvParser>();
            _mockMeterReadingValidator = new Mock<IMeterReadingValidator>();
            _mockMeterReadingDatabaseService = new Mock<IMeterReadingDatabaseService>();
            _meterReadingService = new MeterReadingService(_mockCsvParser.Object, _mockMeterReadingValidator.Object, _mockMeterReadingDatabaseService.Object);
        }

        [Test]
        public async Task GivenAnInvalidMeterReading_WhenProcessUploadedFileIsExecuted_ThenNothingIsStoredInTheDatabase()
        {
            var inputFile = new Mock<IFormFile>();

            _mockCsvParser.Setup(m => m.ParseFile<MeterReadingStrings>(It.IsAny<IFormFile>())).Returns(It.IsAny<List<MeterReadingStrings>>());

            int successCount = 0;
            int failCount = 1;
            _mockMeterReadingValidator.Setup(m => m.Validate(It.IsAny<List<MeterReadingStrings>>(), out successCount, out failCount)).Returns(new List<MeterReading>());

            await _meterReadingService.ProcessUploadedFile(inputFile.Object);

            _mockMeterReadingDatabaseService.Verify(m => m.SaveMeterReadingsAsync(It.IsAny<List<MeterReading>>()), Times.Never());
        }

        [Test]
        public async Task GivenMeterReading_WhenProcessUploadedFileIsExecuted_ThenTheResponseHasTheCounts()
        {
            var inputFile = new Mock<IFormFile>();

            _mockCsvParser.Setup(m => m.ParseFile<MeterReadingStrings>(It.IsAny<IFormFile>())).Returns(It.IsAny<List<MeterReadingStrings>>());

            int successCount = 5;
            int failCount = 0;
            _mockMeterReadingValidator.Setup(m => m.Validate(It.IsAny<List<MeterReadingStrings>>(), out successCount, out failCount)).Returns(new List<MeterReading>());

            var expectedResponse = new MeterReadingUploadResponse()
            {
                SuccessfulReadingCount = successCount,
                FailedReadingCount = failCount
            };

            var actual = await _meterReadingService.ProcessUploadedFile(inputFile.Object);

            actual.Should().BeEquivalentTo(expectedResponse);
        }
    }
}