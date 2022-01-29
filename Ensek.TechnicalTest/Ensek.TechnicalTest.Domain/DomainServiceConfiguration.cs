using Ensek.TechnicalTest.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ensek.TechnicalTest.Domain
{
    public static class DomainServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMeterReadingService, MeterReadingService>();
            services.AddSingleton<ICsvParser, CsvParser>();
            services.AddScoped<IMeterReadingValidator, MeterReadingValidator>();
        }
    }
}
