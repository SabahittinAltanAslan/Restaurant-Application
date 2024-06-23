using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ProgMaui.Entities;

namespace ProgMaui.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7122") };
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("/products");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Product>>();
        }

        public async Task AddProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("/products", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProductAsync(int id, Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"/products/{id}", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/products/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var response = await _httpClient.GetAsync("/orders");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Order>>();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("/categories");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Category>>();
        }
    }
}
