using MagicItems.Shared.Models;
using System.Net.Http.Json;

namespace MagicItems.UI.Services
{
    public class ItemsService
    {

        private readonly HttpClient _httpClient;

        public ItemsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //{Id}/{CategoryId}/{SearchItemName}/{MaxPrice}/{MinPrice}/{RarityId}
        public async Task<Items[]> GetItemsAsync(int Id = 0, int CategoryId = 0, string SearchItemName = "none", double MaxPrice = 0, double MinPrice = 0, int RarityId = 0)
        {
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync($"/Item/GetItems/{Id}/{CategoryId}/{SearchItemName}/{MaxPrice}/{MinPrice}/{RarityId}");
                responseMessage.EnsureSuccessStatusCode();
                return await responseMessage.Content.ReadFromJsonAsync<Items[]>();

            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error fetching categories from the API.", ex);
            }
        }
    }
}
