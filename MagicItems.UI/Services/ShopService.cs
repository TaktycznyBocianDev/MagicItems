using MagicItems.Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace MagicItems.UI.Services
{
    public class ShopService
    {

        private readonly HttpClient _httpClient;

        public ShopService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Shop[]> GetShopsAsync(string shopName = "none")
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/Shop/GetShops/{shopName}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Shop[]>();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error fetching shops from the API.", ex);
            }
        }

        public async Task AddShopAsync(ShopDTO shopDTO)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(shopDTO), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient.PostAsync($"/Shop/CreateShop", jsonContent);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error creating shop", ex);
            }
        }

        public async Task UpdateShopAsync(int shopId, ShopDTO shopToUpdate)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(shopToUpdate), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient.PutAsync($"/Shop/UpdateShop/{shopId}", jsonContent);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error updating shop", ex);
            }
        }

        public async Task DeleteShopAsync(string shopName)
        {
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"/Shop/DeleteShop/{shopName}");
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error deleting shop", ex);
            }
        }
    }
}

