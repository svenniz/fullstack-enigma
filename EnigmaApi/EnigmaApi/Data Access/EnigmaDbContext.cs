using EnigmaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Data_Access
{
    public class EnigmaDbContext : DbContext
    {
        public EnigmaDbContext(DbContextOptions<EnigmaDbContext> options): base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (Database.IsInMemory())
            {
                Console.WriteLine($"Using In-Memory Db, the Database Provider: {Database.ProviderName}");
                Console.WriteLine("Seeding Test Data now:");
                SeedData.SeedTestData(modelBuilder);
            }
            else if (Database.IsSqlite())
            {
                Console.WriteLine("Seeding Real Data now:");
                SeedData.SeedRealData(modelBuilder);
            }
        }
    }
}
