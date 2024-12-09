using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Data_Access
{
    public static class SeedaData
    {
        public static void SeedData(ModelBuilder builder)
        {
            builder.Entity<EnigmaDbContext>().HasData(
                /*Write Seed Data here*/
                );
        }
    }
}
