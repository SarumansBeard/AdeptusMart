﻿using AdeptusMart03.BusinessAccessLayer.Services;
using AdeptusMart06.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart06.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ProductService _productService;
        public AccountController(ProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var model = new AccountViewModel
            {
                Categories = _productService.GetCategoriesAsync().Result
            };
            return View();
        }
    }
}
