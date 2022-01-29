using Ensek.TechnicalTest.Database.Models;

namespace Ensek.TechnicalTest.Database.Interfaces
{
    public interface IMeterReadingDataAccess
    {
        Task SaveMeterReadingsAsync(IEnumerable<MeterReading> meterReadings);

        Task<bool> MeterReadingAlreadyExistsAsync(Domain.Models.MeterReading meterReading);
    }
}
