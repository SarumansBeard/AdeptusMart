﻿using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart04.WebUI.Areas.Management.Controllers
{
    [Area("Management")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
