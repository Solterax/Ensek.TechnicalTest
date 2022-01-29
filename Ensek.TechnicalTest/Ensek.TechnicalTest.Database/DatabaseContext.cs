using Ensek.TechnicalTest.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Ensek.TechnicalTest.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<MeterReading> MeterReadings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeterReading>().HasOne(x => x.Account).WithMany();
        }
    }
}
