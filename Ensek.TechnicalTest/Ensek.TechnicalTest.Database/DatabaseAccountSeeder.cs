using Ensek.TechnicalTest.Database.Interfaces;
using Ensek.TechnicalTest.Database.Models;
using Ensek.TechnicalTest.Domain.Interfaces;

namespace Ensek.TechnicalTest.Database
{
    public class DatabaseAccountSeeder
    {
        private readonly IAccountDataAccess _accountDataAccess;
        private readonly ICsvParser _csvParser;
        private readonly string _seedFile = "Test_Accounts.csv";

        public DatabaseAccountSeeder(IAccountDataAccess accountDataAccess, ICsvParser csvParser)
        {
            _accountDataAccess = accountDataAccess;
            _csvParser = csvParser;
        }

        public async Task Seed()
        {
            var fullSeedFilePath = Path.Combine(Directory.GetCurrentDirectory(), _seedFile);

            var accounts = _csvParser.ParseFromPath<Account>(fullSeedFilePath);

            await _accountDataAccess.SaveAccountDetailsAsync(accounts);
        }
    }
}
