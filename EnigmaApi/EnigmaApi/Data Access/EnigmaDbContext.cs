using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Data_Access
{
    public class EnigmaDbContext : DbContext
    {
        public EnigmaDbContext(DbContextOptions<EnigmaDbContext> options): base(options) { }

        //public DbSet<>  { get; set; };

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
