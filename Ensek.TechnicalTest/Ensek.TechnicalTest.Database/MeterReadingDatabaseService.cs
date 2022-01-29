using Ensek.TechnicalTest.Database.Interfaces;
using Ensek.TechnicalTest.Domain.Interfaces;
using Ensek.TechnicalTest.Domain.Models;

namespace Ensek.TechnicalTest.Database
{
    public class MeterReadingDatabaseService : IMeterReadingDatabaseService
    {
        private readonly IMeterReadingDataAccess _meterReadingDataAccess;
        public MeterReadingDatabaseService(IMeterReadingDataAccess meterReadingDataAccess)
        {
            _meterReadingDataAccess = meterReadingDataAccess;
        }

        public async Task<bool> MeterReadingAlreadyExists(MeterReading meterReading)
        {
            return await _meterReadingDataAccess.MeterReadingAlreadyExistsAsync(meterReading);
        }

        public async Task SaveMeterReadingsAsync(IEnumerable<MeterReading> meterReadings)
        {
            var databaseMeterReadings = new List<Models.MeterReading>();

            foreach (var meterReading in meterReadings)
            {
                databaseMeterReadings.Add(new Models.MeterReading(meterReading));
            }

            await _meterReadingDataAccess.SaveMeterReadingsAsync(databaseMeterReadings);
        }
    }
}
