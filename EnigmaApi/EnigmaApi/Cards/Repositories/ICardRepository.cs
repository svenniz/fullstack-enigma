﻿using EnigmaApi.Cards.Models;
using EnigmaApi.Shared.Repositories;

namespace EnigmaApi.Cards.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<IEnumerable<Card>> GetAllCardsAsync();
        Task<Card?> GetCardAsync(int id);
        Task<IEnumerable<Card>> GetCardsByNames(IEnumerable<string> names);
    }
}
