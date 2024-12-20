namespace EnigmaApi.Models
{
    public class DeckCard
    {
        public int DeckId { get; set; }
        public Deck Deck { get; set; } = null!;

        public int CardId { get; set; }
        public Card Card { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
