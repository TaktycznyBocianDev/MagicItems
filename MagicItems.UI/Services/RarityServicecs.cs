using MagicItems.Shared.Models;
using System.Net.Http.Json;

namespace MagicItems.UI.Services
{
    public class RarityServicecs
    {

        private readonly HttpClient _httpClient;

        public RarityServicecs(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Rarities[]> GetRaritiesAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("Rarity/GetRarity/0");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Rarities[]>();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error fetching rarities from the API.", ex);
            }
        }

    }
}
