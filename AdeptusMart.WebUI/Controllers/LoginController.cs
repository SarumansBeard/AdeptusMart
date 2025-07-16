using AdeptusMart.Business.Services;
using AdeptusMart04.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart04.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ProductService _productService;

        public LoginController(ProductService productService)
        {
            _productService = productService;
        }
        public async Task <IActionResult> Index()
        {
            
            var categories = await _productService.GetCategoriesAsync();
            

            var model = new LoginViewModel
            {
                
                Categories = categories
            };

            return View(model);
        }
    }
}
