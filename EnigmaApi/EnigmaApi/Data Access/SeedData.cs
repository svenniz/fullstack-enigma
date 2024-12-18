using EnigmaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Data_Access
{
    public static class SeedData
    {
        public static void SeedTestData(EnigmaDbContext context)
        {
            // Insert test data for InMemory Db
            var products = new List<Card>
            {
                new Card { Name = "Test Product 1", Price = 10 },
                new Card { Name = "Test Product 2", Price = 20 },
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            context.SaveChanges();
            Console.WriteLine("Test Seed data has been added.");
        }
        public static void SeedRealData(EnigmaDbContext context)
        {
            // Insert real data for SQLite/MySql Db
            var products = new List<Card>
            {
                new Card { Name = "Product 1", Price = 100 },
                new Card { Name = "Product 2", Price = 200 },
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            // Using try catch for seeding real database
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
