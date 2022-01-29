namespace Ensek.TechnicalTest.Domain.Models
{
    public class MeterReading
    {
        public MeterReading(){}

        public MeterReading(MeterReadingStrings meterReadingString)
        {
            AccountId = int.Parse(meterReadingString.AccountId);
            MeterReadingDateTime = DateTime.Parse(meterReadingString.MeterReadingDateTime);
            MeterReadValue = int.Parse(meterReadingString.MeterReadValue);
        }

        public int AccountId { get; set; }

        public DateTime MeterReadingDateTime { get; set; }

        public int MeterReadValue { get; set; }
    }
}
