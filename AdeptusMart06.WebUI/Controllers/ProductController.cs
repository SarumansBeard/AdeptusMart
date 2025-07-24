
using AdeptusMart03.BusinessAccessLayer.Services;
using AdeptusMart06.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AdeptusMart06.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            var categories = await _productService.GetCategoriesAsync();
            var randomProducts = await _productService.GetRandomProductsExceptAsync(id);

            var model = new ProductDetailsViewModel
            {
                ProductDetail = product,
                RandomProducts = randomProducts,
                Categories = categories
            };

            return View(model);
        }
    }
}

