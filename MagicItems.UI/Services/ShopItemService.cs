using MagicItems.Shared.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace MagicItems.UI.Services
{
    public class ShopItemService
    {

        private readonly HttpClient _httpClient;

        public ShopItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Items[]> GetShopsAsync(string shopName)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/ShopItemRelation/GetItemsInShop/{shopName}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Items[]>();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error fetching shops from the API.", ex);
            }
        }

        public async Task AddOneItemToShopAsync(string shopName, string itemName)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(shopName), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient.PutAsync($"/ShopItemRelation/AddOneItemToShop/{shopName}/{itemName}", jsonContent);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error adding one item to shop", ex);
            }
        }


        public async Task AddItemListToShopAsync(string shopName, Items[] items)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(items), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient.PutAsync($"/ShopItemRelation/AddItemListToShop/{shopName}", jsonContent);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error adding list of item to shop", ex);
            }
        }

        public async Task AddItemDTOsToShopAsync(string shopName, ItemsDTO[] items)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(items), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient.PutAsync($"/ShopItemRelation/AddItemDTOsToShop/{shopName}", jsonContent);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error adding one item to shop", ex);
            }
        }

        public async Task RemoveItemFromShopAsync(string ShopName, string ItemName)
        {
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"/ShopItemRelation/RemoveItemFromShop/{ShopName}/{ItemName}");
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error deleting items from shop", ex);
            }
        }
    }
}

