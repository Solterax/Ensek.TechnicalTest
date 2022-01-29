using Ensek.TechnicalTest.Database.Interfaces;
using Ensek.TechnicalTest.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Ensek.TechnicalTest.Database.DataAccess
{
    public class MeterReadingDataAccess : IMeterReadingDataAccess
    {
        private readonly DatabaseContext _database;

        public MeterReadingDataAccess(DatabaseContext database)
        {
            _database = database;
        }

        public async Task<bool> MeterReadingAlreadyExistsAsync(Domain.Models.MeterReading meterReading)
        {
            var existingMeterReading = await _database.MeterReadings.Where(
                x => x.AccountId == meterReading.AccountId &&
                x.MeterReadingDateTime == meterReading.MeterReadingDateTime &&
                x.MeterReadingValue == meterReading.MeterReadValue)
                .FirstOrDefaultAsync();

            return existingMeterReading != null;
        }

        public async Task SaveMeterReadingsAsync(IEnumerable<MeterReading> meterReadings)
        {
            _database.MeterReadings.AddRange(meterReadings);

            await _database.SaveChangesAsync();
        }
    }
}
