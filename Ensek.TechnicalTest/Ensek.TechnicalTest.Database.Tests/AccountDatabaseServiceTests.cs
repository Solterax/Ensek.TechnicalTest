using Ensek.TechnicalTest.Database.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ensek.TechnicalTest.Database.Tests
{
    public class AccountDatabaseServiceTests
    {
        private AccountDatabaseService _accountDatabaseService;
        private Mock<IAccountDataAccess> _mockAccountDataAccess;

        [SetUp]
        public void Setup()
        {
            _mockAccountDataAccess = new Mock<IAccountDataAccess>();
            _accountDatabaseService = new AccountDatabaseService(_mockAccountDataAccess.Object);
        }

        [Test]
        public async Task GivenAListOfAccountIds_WhenGetAccountsThatExistAsyncExecutes_TheReleventAccountsAreReturned()
        {
            var inputId = new List<int>() { 1 };
            var databaseAccounts = new List<Models.Account>()
            {
                new Models.Account()
                {
                    AccountId = 1,
                    FirstName = "Test",
                    LastName = "Account",
                }
            };

            var expectedDomainAccounts = new List<Domain.Models.Account>()
            {
                new Domain.Models.Account()
                {
                    AccountId = databaseAccounts[0].AccountId,
                    FirstName = databaseAccounts[0].FirstName,
                    LastName = databaseAccounts[0].LastName,
                }
            };

            _mockAccountDataAccess.Setup(m => m.GetAccountsAsync(inputId).Result).Returns(databaseAccounts);

            var actual = _accountDatabaseService.GetAccountsThatExistAsync(inputId).Result;

            actual.Should().BeEquivalentTo(expectedDomainAccounts);
        }
    }
}
