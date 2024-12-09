using EnigmaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Data_Access
{
    public static class SeedData
    {
        public static void SeedTestData(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(new List<Product>
            {
                new Product {
                    Id = 1,
                    Name = "Test Product 1",
                    Price = 10
                },
                new Product {
                    Id = 2,
                    Name = "Test Product 2",
                    Price= 20
                }
            });
            Console.WriteLine("Test Seed data has been added.");
        }
        public static void SeedRealData(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(new List<Product>
            {
                new Product {
                    Id = 1,
                    Name = "Real Product 1",
                    Price = 100
                },
                new Product {
                    Id = 2,
                    Name = "Real Product 2",
                    Price= 200
                }
            });
            Console.WriteLine("Real Seed data has been added.");
        }
    }
}
