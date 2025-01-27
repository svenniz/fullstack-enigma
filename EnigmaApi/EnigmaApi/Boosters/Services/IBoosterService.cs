using EnigmaApi.Boosters.Models;

namespace EnigmaApi.Boosters.Services
{
    public interface IBoosterService
    {
        Task<Booster> CreateBoosterAsync(string filePath = null);
    }
}