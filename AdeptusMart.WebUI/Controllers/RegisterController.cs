using AdeptusMart.Business.Services;
using AdeptusMart01.Core.Entities;
using AdeptusMart03.BusinessAccessLayer.Services;
using AdeptusMart04.WebUI.Models;
using AdeptusMart05.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart04.WebUI.Controllers
{
    public class RegisterController : BaseSignInController
    {
        private readonly ProductService _productService;
        private readonly RegisterService _registerService;

        public RegisterController(ProductService productService, RegisterService registerService)
        {
            _productService = productService;
            _registerService = registerService;
        }
        public async Task<IActionResult> Index()
        {

            var categories = await _productService.GetCategoriesAsync();


            var model = new RegisterViewModel
            {
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
         public async Task<IActionResult> Register (RegisterViewModel model)
        {
            bool IsSuccess;

            try
            {
                IsSuccess = await _registerService.Register(model.Account);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                model.IsRegisterSuccess = false;
                return View("Index", model);
            }

            if (IsSuccess)
            {
                return RedirectToAction("Index", "Login");
            }
            TempData["IsRegisterSuccess"] = false;
            return RedirectToAction("Index", "Register");

        }
    }
}
