using EnigmaApi.Cards.Models;

namespace EnigmaApi.Boosters.Models
{
    public class Booster
    {
        public int Id { get; set; }
        public ICollection<Card> Cards { get; set; } // Navigational property
    }
}