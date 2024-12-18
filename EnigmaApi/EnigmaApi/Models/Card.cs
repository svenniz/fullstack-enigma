namespace EnigmaApi.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ManaCost { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Power { get; set; }
        public string? Toughness { get; set; }
        public string Text { get; set; } = string.Empty;
        public string? FlavorText { get; set; }
        public string Rarity { get; set; } = string.Empty;
        public string Set { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        
        public ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>();
    }

}
