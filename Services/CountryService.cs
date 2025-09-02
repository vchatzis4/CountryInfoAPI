using System.Text.Json;
using CountryInfoAPI.Models;
using CountryInfoAPI.Interfaces;

namespace CountryInfoAPI.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CountryService> _logger;
        private const string BaseUrl = "https://restcountries.com/v3.1";

        public CountryService(HttpClient httpClient, ILogger<CountryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Country> GetCountryByNameAsync(string name)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/name/{Uri.EscapeDataString(name)}?fullText=true");
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Country not found: {name}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<Country>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return countries?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching country data for: {name}");
                throw;
            }
        }

        public async Task<List<string>> GetAllCountryNamesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/all?fields=name");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<Country>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return countries?.Select(c => c.Name.Common).OrderBy(n => n).ToList() ?? new List<string>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching country names");
                throw;
            }
        }

        public async Task<Dictionary<string, List<string>>> GetCountriesByRegionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/all?fields=name,region");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<Country>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var countriesByRegion = countries?
                    .Where(c => !string.IsNullOrEmpty(c.Region))
                    .GroupBy(c => c.Region)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(c => c.Name.Common).OrderBy(n => n).ToList()
                    ) ?? new Dictionary<string, List<string>>();

                return countriesByRegion;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching countries by region");
                throw;
            }
        }

        public async Task<Dictionary<string, Currency>> GetCountryCurrencyAsync(string name)
        {
            try
            {
                var country = await GetCountryByNameAsync(name);
                
                if (country == null || country.Currencies == null)
                {
                    return null;
                }

                return country.Currencies;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching currency data for: {name}");
                throw;
            }
        }
    }
}