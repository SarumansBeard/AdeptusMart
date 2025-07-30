using AdeptusMart03.BusinessAccessLayer.Services;
using AdeptusMart04.WebUI.Models;
using AdeptusMart05.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart04.WebUI.Controllers
{
    public class AccountController : BaseSignInController
    {
        private readonly ProductService _productService;
        public AccountController(ProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _productService.GetCategoriesAsync();
            
        var model = new AccountViewModel
            {
                Categories = categories
            };
            return View(model);
        }
    }
}
