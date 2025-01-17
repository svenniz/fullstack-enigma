namespace EnigmaApi.Models
{
    public class Card
    {
        public int Id { get; set; }

        // Game Mechanic Properties
        public string Name { get; set; } = string.Empty;
        public string? ManaCost { get; set; } = string.Empty;
        public string? Type { get; set; } = string.Empty;
        public int? Power { get; set; }
        public int? Toughness { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? Cmc {  get; set; }

        // Other Properties
        public string? SetCode { get; set; }
        public string? SetName { get; set; }
        public string? Language { get; set; } = string.Empty;
        public string? Rarity { get; set; } = string.Empty;
        public string? ArtistName {  get; set; }
        public string? ReleasedAt { get; set; }

        // Navigational Properties
        public ICollection<Image>? Images { get; set; }
        public ICollection<DeckCard> DeckCards { get; set; } = new List<DeckCard>(); // many to many
        public int? BoosterId { get; set; } 
        public Booster? Booster { get; set; } // one to many
    }
}