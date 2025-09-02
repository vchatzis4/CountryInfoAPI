using Microsoft.AspNetCore.Mvc;
using CountryInfoAPI.Interfaces;
using CountryInfoAPI.Models;

namespace CountryInfoAPI.Controllers
{
    /// <summary>
    /// Controller for retrieving country information
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(ICountryService countryService, ILogger<CountriesController> logger)
        {
            _countryService = countryService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all countries grouped by region
        /// </summary>
        /// <returns>A dictionary with regions as keys and lists of country names as values</returns>
        /// <response code="200">Returns the dictionary of regions with country names</response>
        /// <response code="500">If there was an error retrieving the data</response>
        [HttpGet("regions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Dictionary<string, List<string>>>> GetCountriesByRegion()
        {
            try
            {
                var countriesByRegion = await _countryService.GetCountriesByRegionAsync();
                return Ok(countriesByRegion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving countries by region");
                return StatusCode(500, "An error occurred while retrieving countries by region.");
            }
        }

        /// <summary>
        /// Gets all country names
        /// </summary>
        /// <returns>A sorted list of all country names</returns>
        /// <response code="200">Returns the list of country names</response>
        /// <response code="500">If there was an error retrieving the data</response>
        [HttpGet("names")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<string>>> GetCountryNames()
        {
            try
            {
                var countryNames = await _countryService.GetAllCountryNamesAsync();
                return Ok(countryNames);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving country names");
                return StatusCode(500, "An error occurred while retrieving country names.");
            }
        }

        /// <summary>
        /// Gets detailed information about a specific country
        /// </summary>
        /// <param name="name">The name of the country</param>
        /// <returns>Detailed country information</returns>
        /// <response code="200">Returns the country information</response>
        /// <response code="404">If the country was not found</response>
        /// <response code="500">If there was an error retrieving the data</response>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Country>> GetCountryByName(string name)
        {
            try
            {
                var country = await _countryService.GetCountryByNameAsync(name);
                
                if (country == null)
                {
                    return NotFound($"Country '{name}' not found.");
                }

                return Ok(country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving country: {name}");
                return StatusCode(500, "An error occurred while retrieving country information.");
            }
        }

        /// <summary>
        /// Gets currency information for a specific country
        /// </summary>
        /// <param name="name">The name of the country</param>
        /// <returns>Currency information with currency codes as keys</returns>
        /// <response code="200">Returns the currency information</response>
        /// <response code="404">If the country was not found</response>
        /// <response code="500">If there was an error retrieving the data</response>
        [HttpGet("{name}/currency")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Dictionary<string, Currency>>> GetCountryCurrency(string name)
        {
            try
            {
                var currencies = await _countryService.GetCountryCurrencyAsync(name);
                
                if (currencies == null)
                {
                    return NotFound($"Country '{name}' not found or has no currency information.");
                }

                return Ok(currencies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving currency for country: {name}");
                return StatusCode(500, "An error occurred while retrieving currency information.");
            }
        }
    }
}