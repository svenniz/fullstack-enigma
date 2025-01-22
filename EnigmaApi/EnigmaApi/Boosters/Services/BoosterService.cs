using EnigmaApi.Boosters.Models;
using EnigmaApi.Cards.Repositories;
using EnigmaApi.Models;
using EnigmaApi.Shared.Repositories;

namespace EnigmaApi.Boosters.Services
{
    public class BoosterService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IRepository<Booster> _boosterRepository;
        private readonly IRepository<DraftSession> _draftSessionRepository;

        public BoosterService(ICardRepository cardRepository, IRepository<Booster> boosterRepository)
        {
            _cardRepository = cardRepository;
            _boosterRepository = boosterRepository;
        }

    }
}