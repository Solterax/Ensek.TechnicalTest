using Ensek.TechnicalTest.Database.Interfaces;
using Ensek.TechnicalTest.Domain.Interfaces;
using Ensek.TechnicalTest.Domain.Models;

namespace Ensek.TechnicalTest.Database
{
    public class AccountDatabaseService : IAccountDatabaseService
    {
        private readonly IAccountDataAccess _accountDataAccess;

        public AccountDatabaseService(IAccountDataAccess accountDataAccess)
        {
            _accountDataAccess = accountDataAccess;
        }

        public async Task<List<Account>> GetAccountsThatExistAsync(IEnumerable<int> requiredAccountIds)
        {
            var databaseAccounts = await _accountDataAccess.GetAccountsAsync(requiredAccountIds);

            var domainAccounts = new List<Account>();

            foreach (var databaseAccount in databaseAccounts)
            {
                domainAccounts.Add(databaseAccount.GetDomainModel());
            }

            return domainAccounts;
        }
    }
}
