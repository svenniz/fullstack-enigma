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
                SeedaData.SeedTestData(modelBuilder);
            }
            else if (Database.IsSqlite())
            {
                SeedaData.SeedRealData(modelBuilder);
            }
        }
    }
}
