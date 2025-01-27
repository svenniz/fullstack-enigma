using EnigmaApi.Boosters.Models;
using EnigmaApi.Cards.Models;
using EnigmaApi.Cards.Repositories;
using EnigmaApi.Cards.Services;
using EnigmaApi.DraftSessions.Models;
using EnigmaApi.Shared.Repositories;

namespace EnigmaApi.Boosters.Services
{
    public class BoosterService : IBoosterService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IRepository<Booster> _boosterRepository;
        private readonly IRepository<DraftSession> _draftSessionRepository;
        private readonly ICardFileService _cardFileService;
        private readonly DraftSession _draftSession;


        public BoosterService(ICardRepository cardRepository, IRepository<Booster> boosterRepository, ICardFileService cardFileService, DraftSession draftSession)
        {
            _cardRepository = cardRepository;
            _boosterRepository = boosterRepository;
            _cardFileService = cardFileService;
            _draftSession = draftSession;
        }

        public async Task<Booster> CreateBoosterAsync(string filePath = null)
        {
            IEnumerable<Card> availableCards;

            if (!string.IsNullOrEmpty(filePath)) // If a card list is provided (e.g., from a .txt file)
            {
                var cardNames = await _cardFileService.ReadCardNamesFromFileAsync(filePath); // Read card names from file
                availableCards = await _cardRepository.GetCardsByNames(cardNames); // Fetch cards by names
            }
            else
            {
                availableCards = await _cardRepository.GetAllCardsAsync(); // Fetch all cards if no specific list
            }

            var unusedCards = availableCards.Where(card=> !_draftSession.UsedCardIds.Contains(card.Name)).ToList(); // Filter out used cards

            //if (!unusedCards.Any()) // If no unused cards are available
            //{
            //    _draftSession.UsedCardIds.Clear(); // Clear used card list
            //    unusedCards = availableCards.ToList(); // Use all cards
            //}

            // Shuffle and pick 15 random cards
            var random = new Random();
            var boosterCards = unusedCards.OrderBy(c => random.Next()).Take(15).ToList();

            // Add selected cards to used card list
            foreach (var card in boosterCards)
            {
                _draftSession.UsedCardIds.Add(card.Name);
            }

            return new Booster
            {
                Cards = boosterCards
            };
        }
    }
}