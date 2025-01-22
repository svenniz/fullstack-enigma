using EnigmaApi.Decks.Models;
using System.Text;

namespace EnigmaApi.Decks.Services
{
    public class DeckService : IDeckService
    {
        public string GenerateDeckExport(Deck deck)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Deck Name: " + deck.Name);
            sb.AppendLine("Description: " + deck.Description);
            sb.AppendLine("Deck Id: " + deck.Id);
            sb.AppendLine();
            sb.AppendLine("Cards:");
            foreach (var deckCard in deck.DeckCards)
            {
                var card = deckCard.Card;
                if (card != null)
                {
                    sb.AppendLine($"- {deckCard.Card.Name} x{deckCard.Quantity}");
                }
            }
            return sb.ToString();
        }
    }
}
