using MagicItems.Shared.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MagicItems.UI.Services
{


    public class CategoryService
    {

        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Categories[]> GetCategoriesAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("Category/GetCategory/0/none");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Categories[]>();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error fetching categories from the API.", ex);
            }
        }

    }
}
