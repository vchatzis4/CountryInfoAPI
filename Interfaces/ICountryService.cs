using CountryInfoAPI.Models;

namespace CountryInfoAPI.Interfaces
{
    public interface ICountryService
    {
        Task<Country> GetCountryByNameAsync(string name);
        Task<List<string>> GetAllCountryNamesAsync();
        Task<Dictionary<string, List<string>>> GetCountriesByRegionAsync();
        Task<Dictionary<string, Currency>> GetCountryCurrencyAsync(string name);
    }
}