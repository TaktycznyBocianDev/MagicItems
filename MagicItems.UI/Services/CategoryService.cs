using MagicItems.Shared.Models;
using MagicItems.Shared.DTOs;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text;
using System.Text.Json;

namespace MagicItems.UI.Services
{


    public class CategoryService
    {

        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Category[]> GetCategoriesAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("Category/GetCategory/0/none");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Category[]>();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error fetching categories from the API.", ex);
            }
        }

        public async Task CreateCategoryAsync(CategoryDTO category)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"Category/CreateCategory/{category}", category);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error creating category via the API.", ex);
            }
        }

        public async Task DeleteCategoryAsync(int id, string name)
        {
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"Category/DeleteCategory/{id}/{name}");
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error deleting category", ex);
            }
        }


        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                string catString = JsonSerializer.Serialize(category);
                var content = new StringContent(catString);

                HttpResponseMessage responseMessage = await _httpClient.PutAsync($"Category/UpdateCategory/{category.Id}/{category.CategoryName}", content);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Error deleting category", ex);
            }
        }

    }
}
