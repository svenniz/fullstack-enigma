namespace EnigmaApi.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ManaCost { get; set; } = string.Empty;
        public string? Type { get; set; } = string.Empty;
        public string? SetCode { get; set; }
        public string? SetName { get; set; }
        public string? Rarity { get; set; } = string.Empty;
        public int? Power { get; set; }
        public int? Toughness { get; set; }
        public string? Description { get; set; } = string.Empty;

        // Navigational Properties
        public ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>();
        public ICollection<Image>? Images { get; set; }
    }
}
