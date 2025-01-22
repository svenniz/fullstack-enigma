using EnigmaApi.Boosters.Models;
using EnigmaApi.Cards.Repositories;
using EnigmaApi.DraftSessions.Models;
using EnigmaApi.Shared.Repositories;

namespace EnigmaApi.Boosters.Services
{
    public class BoosterService : IBoosterService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IRepository<Booster> _boosterRepository;
        private readonly IRepository<DraftSession> _draftSessionRepository;

        public BoosterService(ICardRepository cardRepository, IRepository<Booster> boosterRepository)
        {
            _cardRepository = cardRepository;
            _boosterRepository = boosterRepository;
        }

        public async Task<Booster> CreateBoosterAsync()
        {
            var booster = new Booster();
            var cards = await _cardRepository.GetAllCardsAsync();
            booster.Cards = cards.ToList();
            await _boosterRepository.AddAsync(booster);
            return booster;
        }
    }
}