using AdeptusMart.Business.Services;
using AdeptusMart01.Core.Entities;
using AdeptusMart04.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdeptusMart04.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _homeService;

        public HomeController(HomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel model = new IndexViewModel();

            model.BannerLefts = await _homeService.GetBannerLeftsAsync();
            model.BannerRight = await _homeService.GetBannerRightAsync();
            model.Products = await _homeService.GetProductsAsync();
            model.Categories = await _homeService.GetCategoriesAsync();
            model.TrendingBanner = await _homeService.GetTrendingBannerAsync();
            model.bestRatedProducts = await _homeService.GetBestRatedProductsAsync();
            model.trendingProducts = await _homeService.GetRandomTrendingProductsAsync();
            model.latestProducts = await _homeService.GetLatestProductsAsync();

            return View(model);
        }
    }
}
