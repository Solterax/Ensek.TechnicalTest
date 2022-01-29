using Ensek.TechnicalTest.Domain.Interfaces;
using Ensek.TechnicalTest.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Ensek.TechnicalTest.Domain
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly ICsvParser _csvParser;
        private readonly IMeterReadingValidator _meterReadingValidator;
        private readonly IMeterReadingDatabaseService _meterReadingDatabaseService;

        public MeterReadingService(ICsvParser csvParser, IMeterReadingValidator meterReadingValidator, IMeterReadingDatabaseService meterReadingDatabaseService)
        {
            _csvParser = csvParser;
            _meterReadingValidator = meterReadingValidator;
            _meterReadingDatabaseService = meterReadingDatabaseService;
        }

        public async Task<MeterReadingUploadResponse> ProcessUploadedFile(IFormFile uploadedFile)
        {
            var meterReadingStrings = _csvParser.ParseFile<MeterReadingStrings>(uploadedFile);

            var validMeterReadings = _meterReadingValidator.Validate(meterReadingStrings, out int successCount, out int failCount);

            if (validMeterReadings.Any())
            {
                await _meterReadingDatabaseService.SaveMeterReadingsAsync(validMeterReadings);
            }

            return new MeterReadingUploadResponse()
            {
                SuccessfulReadingCount = successCount,
                FailedReadingCount = failCount
            };
        }
    }
}
