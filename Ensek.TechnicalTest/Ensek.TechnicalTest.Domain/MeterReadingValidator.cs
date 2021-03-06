using Ensek.TechnicalTest.Domain.Interfaces;
using Ensek.TechnicalTest.Domain.Models;

namespace Ensek.TechnicalTest.Domain
{
    public class MeterReadingValidator : IMeterReadingValidator
    {
        private readonly IAccountDatabaseService _accountDatabaseService;
        private readonly IMeterReadingDatabaseService _meterReadingDatabaseService;
        private static readonly int _maxPossibleReading = 99999;

        public MeterReadingValidator(IAccountDatabaseService accountDatabaseService, IMeterReadingDatabaseService meterReadingDatabaseService)
        {
            _accountDatabaseService = accountDatabaseService;
            _meterReadingDatabaseService = meterReadingDatabaseService;
        }

        public List<MeterReading> Validate(IEnumerable<MeterReadingStrings> meterReadingStrings, out int successCount, out int failCount)
        {
            var meterReadings = ConvertMeterReadingStrings(meterReadingStrings, out int typeFailCount);

            var validMeterReadings = SanitiseMeterReadings(meterReadings, out int valueFailCount);
            
            failCount = typeFailCount + valueFailCount;
            successCount = meterReadingStrings.Count() - failCount;

            return validMeterReadings;
        }

        private List<MeterReading> SanitiseMeterReadings(List<MeterReading> meterReadings, out int valueFailCount)
        {
            var existingAccounts = _accountDatabaseService.GetAccountsThatExistAsync(
                meterReadings.Select(x => x.AccountId)).Result;

            var validMeterReadings = new List<MeterReading>();

            valueFailCount = 0;

            foreach (var meterReading in meterReadings)
            {

                if (MeterReadingAlreadyExists(meterReading).Result)
                {
                    valueFailCount++;
                    continue;
                }

                if (AccountDoesNotExist(meterReading, existingAccounts))
                {
                    valueFailCount++;
                    continue;
                }

                if (ReadingLargerThanMaxPossibleReading(meterReading))
                {
                    valueFailCount++;
                    continue;
                }

                validMeterReadings.Add(meterReading);
            }

            return validMeterReadings;
        }

        private bool ReadingLargerThanMaxPossibleReading(MeterReading meterReading)
        {
            return meterReading.MeterReadValue > _maxPossibleReading;
        }

        private bool AccountDoesNotExist(MeterReading meterReading, List<Account> existingAccounts)
        {
            return !existingAccounts.Exists(x => x.AccountId == meterReading.AccountId);
        }

        private async Task<bool> MeterReadingAlreadyExists(MeterReading meterReading)
        {
            return await _meterReadingDatabaseService.MeterReadingAlreadyExists(meterReading);
        }

        private List<MeterReading> ConvertMeterReadingStrings(IEnumerable<MeterReadingStrings> meterReadingStrings, out int typeFailCount)
        {
            typeFailCount = 0;

            var meterReadings = new List<MeterReading>();

            foreach (var meterReadingString in meterReadingStrings)
            {
                if (AccountIdIsValid(meterReadingString) &&
                    MeterReadingDateTimeIsValid(meterReadingString) &&
                    MeterReadingValueIsValid(meterReadingString))
                {
                    var meterReading = new MeterReading(meterReadingString);
                    meterReadings.Add(meterReading);
                }
                else
                {
                    typeFailCount++;
                }
            }
            return meterReadings;
        }

        private static bool MeterReadingValueIsValid(MeterReadingStrings meterReadingString)
        {
            return int.TryParse(meterReadingString.MeterReadValue, out _);
        }

        private static bool MeterReadingDateTimeIsValid(MeterReadingStrings meterReadingString)
        {
            return DateTime.TryParse(meterReadingString.MeterReadingDateTime, out _);
        }

        private static bool AccountIdIsValid(MeterReadingStrings meterReadingString)
        {
            return int.TryParse(meterReadingString.AccountId, out _);
        }
    }
}
