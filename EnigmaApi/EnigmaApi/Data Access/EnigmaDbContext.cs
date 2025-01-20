using EnigmaApi.Boosters.Models;
using EnigmaApi.Cards.Models;
using EnigmaApi.DeckCards.Models;
using EnigmaApi.Decks.Models;
using EnigmaApi.Images.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EnigmaApi.Data_Access
{
    public class EnigmaDbContext : DbContext
    {
        public EnigmaDbContext(DbContextOptions<EnigmaDbContext> options): base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<DeckCard> DeckCards { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Booster> Boosters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key for DeckCard (many-to-many relationship between Deck and Card)
            modelBuilder.Entity<DeckCard>()
            .HasKey(dc => new { dc.DeckId, dc.CardId }); // Composite primary key

            // Relationship: DeckCard → Deck (Deck -> DeckCards)
            modelBuilder.Entity<DeckCard>()
                .HasOne(dc => dc.Deck)
                .WithMany(d => d.DeckCards)
                .HasForeignKey(dc => dc.DeckId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: DeckCard → Card (Card -> DeckCards)
            modelBuilder.Entity<DeckCard>()
                .HasOne(dc => dc.Card)
                .WithMany(c => c.DeckCards)
                .HasForeignKey(dc => dc.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Image → Card (Image -> Cards)
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Card)
                .WithMany(c => c.Images)
                .HasForeignKey(i => i.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Card → Booster (One Card belongs to one Booster)
            modelBuilder.Entity<Card>()
                .HasOne(c => c.Booster)
                .WithMany(b => b.Cards)
                .HasForeignKey(c => c.BoosterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
