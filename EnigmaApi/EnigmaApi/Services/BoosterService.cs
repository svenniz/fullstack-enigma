using EnigmaApi.Models;
using EnigmaApi.Repositories;

namespace EnigmaApi.Services
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