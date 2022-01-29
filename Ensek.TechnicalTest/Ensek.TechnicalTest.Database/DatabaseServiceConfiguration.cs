using Ensek.TechnicalTest.Database.DataAccess;
using Ensek.TechnicalTest.Database.Interfaces;
using Ensek.TechnicalTest.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ensek.TechnicalTest.Database
{
    public class DatabaseServiceConfiguration
    {
        public static async Task ConfigureServices(IServiceCollection services, string databaseString)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(databaseString));

            services.AddScoped<IMeterReadingDataAccess, MeterReadingDataAccess>();
            services.AddScoped<IAccountDataAccess, AccountDataAccess>();
            services.AddScoped<IAccountDatabaseService, AccountDatabaseService>();
            services.AddScoped<IMeterReadingDatabaseService, MeterReadingDatabaseService>();

            var seeder = new DatabaseAccountSeeder(
                services.BuildServiceProvider().GetRequiredService<IAccountDataAccess>(),
                services.BuildServiceProvider().GetRequiredService<ICsvParser>());
            await seeder.Seed();
        }
    }
}
