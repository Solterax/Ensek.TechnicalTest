using Ensek.TechnicalTest.Domain.Models;

namespace Ensek.TechnicalTest.Domain.Interfaces
{
    public interface IMeterReadingValidator
    {
        List<MeterReading> Validate(IEnumerable<MeterReadingStrings> meterReadingStrings, out int successCount, out int failCount);
    }
}
