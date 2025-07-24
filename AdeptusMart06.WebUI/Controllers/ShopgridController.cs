using AdeptusMart.Business.Services;
using AdeptusMart.BusinessAccessLayer.Services;
using AdeptusMart06.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart06.WebUI.Controllers
{
    public class ShopgridController : Controller
    {

        public readonly ShopgridService _shopgridService;
       

        public ShopgridController(ShopgridService shopgridService)
        {
            _shopgridService = shopgridService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(List<Guid>? categoryIds = null, List<decimal>? filteredPrice = null, decimal? sortBy = null, string? reset = null)
        {
            ShopgridViewModel model = new ShopgridViewModel();

            ViewBag.SelectedCategories = categoryIds ?? new List<Guid>();
            ViewBag.FilteredPrice = filteredPrice ?? new List<decimal>();
            ViewBag.SortBy =sortBy ?? 0;
            
            model.Products = await _shopgridService.GetFilteredProductsWithCategoryAsync(categoryIds,filteredPrice,sortBy,reset);         
                      
            model.Categories = await _shopgridService.GetAllCategoriesAsync();

            if (reset == "true")
            {
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }

        







    }
}
