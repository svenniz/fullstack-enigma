using EnigmaApi.DeckCards.Models;
using EnigmaApi.Decks.Models;
using EnigmaApi.Decks.Repositories;
using System.Text;

namespace EnigmaApi.Decks.Services
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _deckRepository;
        public DeckService(IDeckRepository deckRepository)
        {
            _deckRepository = deckRepository;
        }

        public async Task AddCardToDeckAsync(int deckId, int cardId)
        {
            var deck = await _deckRepository.GetDeckAsync(deckId);
            if (deck == null)
            {
                throw new ArgumentException("Deck not found");
            }

            var deckCard = deck.DeckCards.FirstOrDefault(dc => dc.CardId == cardId);
            if (deckCard == null)
            {
                deckCard = new DeckCard
                {
                    DeckId = deckId,
                    CardId = cardId,
                    Quantity = 1
                };
                deck.DeckCards.Add(deckCard);
            }
            else
            {
                deckCard.Quantity++;
            }

            await _deckRepository.SaveChanges();
        }

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
