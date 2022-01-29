using Ensek.TechnicalTest.Domain.Models;

namespace Ensek.TechnicalTest.Domain.Interfaces
{
    public interface IAccountDatabaseService
    {
        Task<List<Account>> GetAccountsThatExistAsync(IEnumerable<int> requiredAccountIds);
    }
}