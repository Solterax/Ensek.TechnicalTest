using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ensek.TechnicalTest.Database.Models
{
    [Table(nameof(Account))]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
