using Ensek.TechnicalTest.Database.Interfaces;
using Ensek.TechnicalTest.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Ensek.TechnicalTest.Database.DataAccess
{
    public class AccountDataAccess : IAccountDataAccess
    {
        private readonly DatabaseContext _database;
        public AccountDataAccess(DatabaseContext database)
        {
            _database = database;
        }

        public async Task SaveAccountDetailsAsync(IEnumerable<Account> accounts)
        {
            var newAccounts = accounts.Where(x => !_database.Accounts.Any(y => y.AccountId == x.AccountId));

            _database.Accounts.AddRange(newAccounts);

            await _database.SaveChangesAsync();
        }

        public async Task<List<Account>> GetAccountsAsync(IEnumerable<int> requiredAccountIds)
        {
            return await _database.Accounts.Where(x => requiredAccountIds.Any(r => r == x.AccountId)).ToListAsync();
        }
    }
}
