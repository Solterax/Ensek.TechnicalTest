using Ensek.TechnicalTest.Domain.Interfaces;
using Ensek.TechnicalTest.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ensek.TechnicalTest.Domain.Tests
{
    public class MeterReadingValidatorTests
    {
        private MeterReadingValidator _meterReadingValidator;
        private Mock<IAccountDatabaseService> _mockAccountDatabaseService;
        private Mock<IMeterReadingDatabaseService> _mockMeterReadingDatabaseService;

        [SetUp]
        public void Setup()
        {
            _mockAccountDatabaseService = new Mock<IAccountDatabaseService>();
            _mockMeterReadingDatabaseService = new Mock<IMeterReadingDatabaseService>();
            _meterReadingValidator = new MeterReadingValidator(_mockAccountDatabaseService.Object, _mockMeterReadingDatabaseService.Object);
        }

        [Test]
        public void GivenAnInvalidAccountId_WhenValidated_ThenTheObjectIsNotReturned()
        {
            var inputMeterReadingStrings = new List<MeterReadingStrings>()
            {
                new MeterReadingStrings()
                {
                    AccountId = "string",
                    MeterReadingDateTime = DateTime.UtcNow.ToString(),
                    MeterReadValue = "1"
                }
            };

            int successCount = 0;
            int failCount = 0;
            var actual = _meterReadingValidator.Validate(inputMeterReadingStrings, out successCount, out failCount);

            actual.Should().BeEquivalentTo(new List<MeterReading>());
        }

        [Test]
        public void GivenAnInvalidDateTime_WhenValidated_ThenTheObjectIsNotReturned()
        {
            var inputMeterReadingStrings = new List<MeterReadingStrings>()
            {
                new MeterReadingStrings()
                {
                    AccountId = "1",
                    MeterReadingDateTime = "not a date time",
                    MeterReadValue = "1"
                }
            };

            int successCount = 0;
            int failCount = 0;
            var actual = _meterReadingValidator.Validate(inputMeterReadingStrings, out successCount, out failCount);

            actual.Should().BeEquivalentTo(new List<MeterReading>());
        }

        [Test]
        public void GivenAnInvalidMeterReadValue_WhenValidated_ThenTheObjectIsNotReturned()
        {
            var inputMeterReadingStrings = new List<MeterReadingStrings>()
            {
                new MeterReadingStrings()
                {
                    AccountId = "1",
                    MeterReadingDateTime = DateTime.UtcNow.ToString(),
                    MeterReadValue = "not a number"
                }
            };

            int successCount = 0;
            int failCount = 0;
            var actual = _meterReadingValidator.Validate(inputMeterReadingStrings, out successCount, out failCount);

            actual.Should().BeEquivalentTo(new List<MeterReading>());
        }

        [Test]
        public void GivenAMeterReadingThatAlreadyExists_WhenValidated_ThenTheObjectIsNotReturned()
        {
            var inputMeterReadingStrings = new List<MeterReadingStrings>()
            {
                new MeterReadingStrings()
                {
                    AccountId = "1",
                    MeterReadingDateTime = DateTime.UtcNow.ToString(),
                    MeterReadValue = "123"
                }
            };

            _mockMeterReadingDatabaseService.Setup(m => m.MeterReadingAlreadyExists(It.IsAny<MeterReading>()).Result).Returns(true);

            int successCount = 0;
            int failCount = 0;
            var actual = _meterReadingValidator.Validate(inputMeterReadingStrings, out successCount, out failCount);

            actual.Should().BeEquivalentTo(new List<MeterReading>());
        }

        [Test]
        public void GivenAMeterReadingWhereTheAccountDoesNotExist_WhenValidated_ThenTheObjectIsNotReturned()
        {
            var inputMeterReadingStrings = new List<MeterReadingStrings>()
            {
                new MeterReadingStrings()
                {
                    AccountId = "1",
                    MeterReadingDateTime = DateTime.UtcNow.ToString(),
                    MeterReadValue = "123"
                }
            };

            _mockMeterReadingDatabaseService.Setup(m => m.MeterReadingAlreadyExists(It.IsAny<MeterReading>()).Result).Returns(false);
            _mockAccountDatabaseService.Setup(m => m.GetAccountsThatExistAsync(new List<int>() { 1 }).Result).Returns(new List<Account>());

            int successCount = 0;
            int failCount = 0;
            var actual = _meterReadingValidator.Validate(inputMeterReadingStrings, out successCount, out failCount);

            actual.Should().BeEquivalentTo(new List<MeterReading>());
        }

        [Test]
        public void GivenAMeterReadingWhereMeterReadingValueIsInvalid_WhenValidated_ThenTheObjectIsNotReturned()
        {
            var inputMeterReadingStrings = new List<MeterReadingStrings>()
            {
                new MeterReadingStrings()
                {
                    AccountId = "1",
                    MeterReadingDateTime = DateTime.UtcNow.ToString(),
                    MeterReadValue = "123456789"
                }
            };

            var existingAccounts = new List<Account>()
            {
                new Account()
                {
                    AccountId = 1,
                    FirstName = "Test",
                    LastName = "Account",
                }
            };

            _mockMeterReadingDatabaseService.Setup(m => m.MeterReadingAlreadyExists(It.IsAny<MeterReading>()).Result).Returns(false);
            _mockAccountDatabaseService.Setup(m => m.GetAccountsThatExistAsync(new List<int>() { 1 }).Result).Returns(existingAccounts);

            int successCount = 0;
            int failCount = 0;
            var actual = _meterReadingValidator.Validate(inputMeterReadingStrings, out successCount, out failCount);

            actual.Should().BeEquivalentTo(new List<MeterReading>());
        }

        [Test]
        public void GivenAMeterReadingMeterThatIsValid_WhenValidated_ThenTheMeterReadingObjectIsReturned()
        {
            var inputMeterReadingStrings = new List<MeterReadingStrings>()
            {
                new MeterReadingStrings()
                {
                    AccountId = "1",
                    MeterReadingDateTime = DateTime.UtcNow.ToString(),
                    MeterReadValue = "123"
                }
            };

            var existingAccounts = new List<Account>()
            {
                new Account()
                {
                    AccountId = 1,
                    FirstName = "Test",
                    LastName = "Account",
                }
            };

            var expectedMeterReading = new List<MeterReading>()
            {
                new MeterReading()
                {
                    AccountId = int.Parse(inputMeterReadingStrings[0].AccountId),
                    MeterReadingDateTime = DateTime.Parse(inputMeterReadingStrings[0].MeterReadingDateTime),
                    MeterReadValue = int.Parse(inputMeterReadingStrings[0].MeterReadValue)
                }
            };

            _mockMeterReadingDatabaseService.Setup(m => m.MeterReadingAlreadyExists(It.IsAny<MeterReading>()).Result).Returns(false);
            _mockAccountDatabaseService.Setup(m => m.GetAccountsThatExistAsync(new List<int>() { 1 }).Result).Returns(existingAccounts);

            int successCount = 0;
            int failCount = 0;
            var actual = _meterReadingValidator.Validate(inputMeterReadingStrings, out successCount, out failCount);

            actual.Should().BeEquivalentTo(expectedMeterReading);
        }
    }
}
