namespace EnigmaApi.Models
{
    public class Booster
    {
        public int Id { get; set; }
        public ICollection<Card> Cards { get; set; } // Navigational property
    }
}
