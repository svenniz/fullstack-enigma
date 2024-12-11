using EnigmaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Data_Access
{
    public static class SeedData
    {
        public static void SeedTestData(EnigmaDbContext context)
        {
            if (!context.Products.Any()) // Ensuring there’s no existing data
            {
                // Insert test data for InMemory Db
                context.Products.AddRange(new Product
                {
                    Name = "Test Product 1",
                    Price = 10
                }, new Product
                {
                    Name = "Test Product 2",
                    Price = 20
                });
            }
            context.SaveChanges();
            Console.WriteLine("Test Seed data has been added.");
        }
        public static void SeedRealData(EnigmaDbContext context)
        {
            if (!context.Products.Any()) // Ensuring there’s no existing data
            {
                // Insert real data for SQLite/MySql Db
                context.Products.AddRange(new Product
                {
                    Name = "Real Product 1",
                    Price = 100
                }, new Product
                {
                    Name = "Real Product 2",
                    Price = 200
                });

                try
                {
                    // Code that triggers the update
                    context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                }
                Console.WriteLine("Real Seed data has been added.");
            }
        }
    }
}
