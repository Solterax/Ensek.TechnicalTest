using Ensek.TechnicalTest.Database.Models;

namespace Ensek.TechnicalTest.Database.Interfaces
{
    public interface IAccountDataAccess
    {
        Task SaveAccountDetailsAsync(IEnumerable<Account> accounts);
        Task<List<Account>> GetAccountsAsync(IEnumerable<int> requiredAccountIds);
    }
}
