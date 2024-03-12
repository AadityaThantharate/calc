using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class CalculatorDbContext : DbContext
    {
        public CalculatorDbContext(DbContextOptions<CalculatorDbContext> options)
            : base(options)
        {
        }

        public DbSet<CalculationHistory> CalculationHistories { get; set; }
        public DbSet<CustomCalculation> CustomCalculations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalculationHistory>()
                .Property(h => h.Expression)
                .IsRequired();

            modelBuilder.Entity<CalculationHistory>()
                .Property(h => h.Result)
                .IsRequired();

            modelBuilder.Entity<CalculationHistory>()
                .Property(h => h.DateCreated)
                .IsRequired();

            modelBuilder.Entity<CustomCalculation>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<CustomCalculation>()
                .Property(c => c.Expression)
                .IsRequired();

            modelBuilder.Entity<CustomCalculation>()
                .Property(c => c.DateCreated)
                .IsRequired();
        }
    }
}