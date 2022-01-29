using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ensek.TechnicalTest.Database.Models
{
    [Table(nameof(MeterReading))]
    [Index(nameof(AccountId))]
    [Index(nameof(AccountId), nameof(MeterReadingDateTime))]
    public class MeterReading
    {
        public MeterReading(){}

        public MeterReading(Domain.Models.MeterReading meterReading)
        {
            AccountId = meterReading.AccountId;
            MeterReadingDateTime = meterReading.MeterReadingDateTime;
            MeterReadingValue = meterReading.MeterReadValue;
        }

        [Key]
        public int MeterReadingId { get; set; }

        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }

        public DateTime MeterReadingDateTime { get; set; }

        public int MeterReadingValue { get; set; }

        public virtual Account Account { get; set; }

        public Domain.Models.MeterReading GetDomainModel()
        {
            return new Domain.Models.MeterReading()
            {
                AccountId = AccountId,
                MeterReadingDateTime = MeterReadingDateTime,
                MeterReadValue = MeterReadingValue
            };
        }
    }
}
