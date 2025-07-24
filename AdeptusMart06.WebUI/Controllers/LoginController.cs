using AdeptusMart03.BusinessAccessLayer.Services;
using AdeptusMart06.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart06.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ProductService _productService;
        private readonly LoginService _loginService;

        public LoginController(ProductService productService, LoginService loginService)
        {
            _productService = productService;
            _loginService = loginService;
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

        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel model)
        {
            bool LoginSuccess;

            var username = model.Account.Username;
            var password = model.Account.Password;
            var sessionId = HttpContext.Session.Id;

            LoginSuccess =await _loginService.LogIn(username, password,sessionId);

            try
            {
                if (LoginSuccess == true)
                {
                    TempData["IsLoginSuccess"] = true;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["IsLoginSuccess"] = false;                    
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index");
            }

        }


    }
}
