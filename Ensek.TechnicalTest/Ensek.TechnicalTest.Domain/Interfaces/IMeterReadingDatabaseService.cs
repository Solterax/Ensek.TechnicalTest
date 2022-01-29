using Ensek.TechnicalTest.Domain.Models;

namespace Ensek.TechnicalTest.Domain.Interfaces
{
    public interface IMeterReadingDatabaseService
    {
        Task<bool> MeterReadingAlreadyExists(MeterReading meterReading);

        Task SaveMeterReadingsAsync(IEnumerable<MeterReading> meterReadings);
    }
}