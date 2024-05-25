using MagicItems.Shared.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace MagicItems.UI.Services
{
    public class ItemsService
    {

        private readonly HttpClient _httpClient;

        public ItemsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //[HttpGet("GetItems/{Id}/{SearchItemName}/{MaxPrice}/{MinPrice}/{CategoryName}/{RarityName}")]
        public async Task<Items[]> GetItemsAsync(int Id = 0, string SearchItemName = "none", double MaxPrice = 0, double MinPrice = 0, string CategoryName = "none", string RarityName = "none")
        {
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.GetAsync($"/Item/GetItems/{Id}/{SearchItemName}/{MaxPrice}/{MinPrice}/{CategoryName}/{RarityName}");
                responseMessage.EnsureSuccessStatusCode();
                return await responseMessage.Content.ReadFromJsonAsync<Items[]>();

            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error fetching categories from the API.", ex);
            }
        }

        public async Task DeleteItemAsync(string itemName)
        {
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"/Item/DeleteItem/{itemName}");
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error deleting category", ex);
            }
        }

        public async Task UpdateItemAsync(int Id, ItemsDTO itemAfterUpdate)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(itemAfterUpdate), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient.PutAsync($"/Item/UpdateItem/{Id}", jsonContent);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error updating category", ex);
            }
        }

        public async Task AddItemAsync(ItemsDTO newItem)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(newItem), Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await _httpClient.PostAsync($"/Item/AddItem/{newItem}", jsonContent);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error adding item", ex);
            }
        }

    }
}
