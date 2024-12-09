using EnigmaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Data_Access
{
    public static class SeedaData
    {
        public static void SeedTestData(ModelBuilder builder)
        {
            builder.Entity<EnigmaDbContext>().HasData(new List<Product>
            {
                new Product {
                    Id = 1,
                    Name = "Test Product 1",
                    Price = 10,
                    Secret = ""
                },
                new Product {
                    Id = 2,
                    Name = "Test Product 2",
                    Price= 20,
                    Secret = "little secret"
                }
            });
        }
        public static void SeedRealData(ModelBuilder builder)
        {
            builder.Entity<EnigmaDbContext>().HasData(new List<Product>
            {
                new Product {
                    Id = 1,
                    Name = "Real Product 1",
                    Price = 100,
                    Secret = ""
                },
                new Product {
                    Id = 2,
                    Name = "Real Product 2",
                    Price= 200,
                    Secret = "little secret"
                }
            });
        }
    }
}
