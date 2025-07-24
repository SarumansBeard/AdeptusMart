
using AdeptusMart.Business.Services;
using AdeptusMart01.Core.Entities;
using AdeptusMart03.BusinessAccessLayer.Services;
using AdeptusMart04.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AdeptusMart04.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly HomeService _homeService;
        private readonly HttpClient _httpClient;

        public ProductController(ProductService productService, HomeService homeService)
        {
            _productService = productService;
            _homeService = homeService;
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await GetProductById(id);
            var categories = await GetCategories();
            var randomProducts = await GetRandomProductsExceptAsync(id);

            var model = new ProductDetailsViewModel
            {
                ProductDetail = product,
                RandomProducts = randomProducts,
                Categories = categories
            };

            return View(model);
        }


        public async Task<List<Product>?> GetProducts()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7112/api/product/get");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new List<Product>();
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent content = response.Content;
                string stringContent = await content.ReadAsStringAsync();
                List<Product>? products = JsonConvert.DeserializeObject<List<Product>?>(stringContent);


                return products ?? new List<Product>();
            }
            else
            {
                return null;
            }
        }

        public async Task<Product?> GetProductById(Guid Id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7112/api/product/get/{Id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new Product();
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent content = response.Content;
                string stringContent = await content.ReadAsStringAsync();
                Product? product = JsonConvert.DeserializeObject<Product?>(stringContent);


                return product ?? new Product();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Category>?> GetCategories()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7112/api/categories/get");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new List<Category>();
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent content = response.Content;
                string stringContent = await content.ReadAsStringAsync();
                List<Category>? categories = JsonConvert.DeserializeObject<List<Category>?>(stringContent);


                return categories ?? new List<Category>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Product>> GetRandomProductsExceptAsync(Guid excludedId, int count = 6)
        {
            var products = await GetProducts();
            var filteredProducts = products
                .Where(p => p.Id != excludedId)
                .ToList();

            var random = new Random();
            return filteredProducts.OrderBy(x => random.Next()).Take(count).ToList();
        }
    }
}

