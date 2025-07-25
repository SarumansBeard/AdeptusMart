using AdeptusMart.Business.Services;
using AdeptusMart01.Core.Entities;
using AdeptusMart04.WebUI.Models;
using AdeptusMart05.WebUI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace AdeptusMart04.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        

        public HomeController()
        {            
            _httpClient = new HttpClient();
            
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel model = new IndexViewModel();

            //model.BannerLefts = await _homeService.GetBannerLeftsAsync();
            model.BannerLefts = await GetBannerLefts();
            //model.BannerRight = await _homeService.GetBannerRightAsync();
            model.BannerRight = await GetBannerRight();
            //model.Products = await _homeService.GetProductsAsync();
            model.ProductDTOs = await GetProducts();
            //model.Categories = await _homeService.GetCategoriesAsync();
            model.Categories = await GetCategories();
            //model.TrendingBanner = await _homeService.GetTrendingBannerAsync();
            model.TrendingBanner = await GetTrendingBanner();
            //model.bestRatedProducts = await _homeService.GetBestRatedProductsAsync();
            model.bestRatedProducts = await GetBestRatedProductsAsync();
            //model.trendingProducts = await _homeService.GetRandomTrendingProductsAsync();
            model.trendingProducts = await GetTrendingProducts();
            //model.latestProducts = await _homeService.GetLatestProductsAsync();
            model.latestProducts = await GetLatestProductsAsync();

            return View(model);
        }

        public async Task<List<BannerLeft>?> GetBannerLefts()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7112/api/bannerlefts/get");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new List<BannerLeft>();
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent content = response.Content;
                string stringContent = await content.ReadAsStringAsync();
                List<BannerLeft>? bannerlefts = JsonConvert.DeserializeObject<List<BannerLeft>?>(stringContent);


                return bannerlefts ?? new List<BannerLeft>();
            }
            else
            {
                return null;
            }
        }

        public async Task<BannerRight?> GetBannerRight()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7112/api/bannerright/get");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new BannerRight();
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent content = response.Content;
                string stringContent = await content.ReadAsStringAsync();
                BannerRight? bannerright = JsonConvert.DeserializeObject<BannerRight?>(stringContent);


                return bannerright ?? new BannerRight();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<ProductDTO>?> GetProducts()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7112/api/product/get");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new List<ProductDTO>();
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent content = response.Content;
                string stringContent = await content.ReadAsStringAsync();
                List<ProductDTO>? productDTOs = JsonConvert.DeserializeObject<List<ProductDTO>?>(stringContent);

                

                return productDTOs ?? new List<ProductDTO>();
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

        public async Task<TrendingBanner?> GetTrendingBanner()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7112/api/trendingbanner/get");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new TrendingBanner();
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent content = response.Content;
                string stringContent = await content.ReadAsStringAsync();
                TrendingBanner? trendingBanner = JsonConvert.DeserializeObject<TrendingBanner?>(stringContent);


                return trendingBanner ?? new TrendingBanner();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Product>?> GetBestRatedProductsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7112/api/bestRatedProducts/get");
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
                List<Product>? bestRatedProducts = JsonConvert.DeserializeObject<List<Product>?>(stringContent);
                return bestRatedProducts ?? new List<Product>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Product>?> GetTrendingProducts()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7112/api/RandomTrendingProducts/get");
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
                List<Product>? trendingproducts = JsonConvert.DeserializeObject<List<Product>?>(stringContent);


                return trendingproducts ?? new List<Product>();
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Product>?> GetLatestProductsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7112/api/latestProducts/get");
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
                List<Product>? latestProducts = JsonConvert.DeserializeObject<List<Product>?>(stringContent);

                return latestProducts ?? new List<Product>();
            }
            else
            {
                return null;
            }
        }

        public async Task<int?> ItemCountByCategory()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7112/api/itemcountbycategory/get");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new int();
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent content = response.Content;
                string stringContent = await content.ReadAsStringAsync();
                int? itemcountbycategory = JsonConvert.DeserializeObject<int?>(stringContent);


                return itemcountbycategory ?? new int();
            }
            else
            {
                return null;
            }
        }

    }
}
