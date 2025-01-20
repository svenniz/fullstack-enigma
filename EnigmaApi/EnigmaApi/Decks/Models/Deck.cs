using EnigmaApi.DeckCards.Models;

namespace EnigmaApi.Decks.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        //public int? PlayerId { get; set; }
        //public Player? Player { get; set; } // Navigation property
        public ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>(); // Navigation property
    }
}
