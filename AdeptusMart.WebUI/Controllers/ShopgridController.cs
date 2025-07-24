using AdeptusMart.Business.Services;
using AdeptusMart.BusinessAccessLayer.Services;
using AdeptusMart01.Core.Entities;
using AdeptusMart04.WebUI.Models;
using AdeptusMart05.Api.DTOs;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace AdeptusMart04.WebUI.Controllers
{
    public class ShopgridController : Controller
    {

        public readonly ShopgridService _shopgridService;
        private readonly HttpClient _httpClient;


        public ShopgridController(ShopgridService shopgridService)
        {
            _shopgridService = shopgridService;
            _httpClient = new HttpClient();
        }


        [HttpGet]
        public async Task<IActionResult> Index(List<Guid>? categoryIds = null, List<decimal>? filteredPrice = null, decimal? sortBy = null, string? reset = null)
        {
            ShopgridViewModel model = new ShopgridViewModel();

            ViewBag.SelectedCategories = categoryIds ?? new List<Guid>();
            ViewBag.FilteredPrice = filteredPrice ?? new List<decimal>();
            ViewBag.SortBy =sortBy ?? 0;

            using (var httpClient = new HttpClient())
            {
                
                var baseUrl = "https://localhost:7112/api/productfiltered/get";

                var queryParams = new List<string>();

                if (categoryIds != null)
                {
                    foreach (var id in categoryIds)
                        queryParams.Add($"categoryIds={id}");
                }

                if (filteredPrice != null)
                {
                    foreach (var price in filteredPrice)
                        queryParams.Add($"filteredPrice={price}");
                }

                if (sortBy != null)
                    queryParams.Add($"sortBy={sortBy}");

                if (!string.IsNullOrEmpty(reset))
                    queryParams.Add($"reset={reset}");

                var queryString = string.Join("&", queryParams);
                var fullUrl = $"{baseUrl}?{queryString}";

                var response = await httpClient.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    model.Products = JsonConvert.DeserializeObject<List<Product>>(json);
                }
                else
                {
                    // Hata varsa boş liste gönder veya logla
                    model.Products = new List<Product>();
                }
            }
            model.Categories = await _shopgridService.GetAllCategoriesAsync();

            if (reset == "true")
            {
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }


    }
}
