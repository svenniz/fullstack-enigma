using EnigmaApi.Boosters.Models;

namespace EnigmaApi.Models
{
    public class DraftSession
    {
        public int Id { get; set; }
        public List<Booster> Boosters { get; set; } = new List<Booster>();
        public HashSet<string> UsedCardIds { get; set; } = new HashSet<string>(); // Used to keep track of which cards have been used
    }
}
