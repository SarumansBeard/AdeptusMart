using AdeptusMart01.Core.Entities;
using AdeptusMart03.BusinessAccessLayer.Services;
using AdeptusMart04.WebUI.Models;
using AdeptusMart05.WebUI.Controllers;
using AdeptusMart05.WebUI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AdeptusMart04.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ProductService _productService;
        private readonly LoginService _loginService;
        private readonly HttpClient _httpClient;

        public LoginController(ProductService productService, LoginService loginService)
        {
            _productService = productService;
            _httpClient = new HttpClient();
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
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            var accountDto = new AccountDTO
            {
                Username = model.Account.Username,
                Password = model.Account.Password
            };

            var Json= JsonConvert.SerializeObject(accountDto);
            var content = new StringContent(Json, Encoding.UTF8, "application/json");

            Console.WriteLine(await content.ReadAsStringAsync());

            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7112/api/account/login",content);


            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                TempData["IsLoginSuccess"] = false;
                return View(model);
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                TempData["IsLoginSuccess"] = false;
                return View(model);
            }
            else if (response.StatusCode == HttpStatusCode.OK)
            {
                
                string stringContent = await response.Content.ReadAsStringAsync();
                Guid? accountId = JsonConvert.DeserializeObject<Guid?>(stringContent);

                try
                {
                    if (accountId != null)
                    {
                        TempData["IsLoginSuccess"] = true;
                        HttpContext.Session.SetString("UserId", accountId.ToString());
                        Console.WriteLine($"Kullanıcı Giriş Başarılı: {accountId}");
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
            else
            {
                return View(model);
            }            

        }


    }
}
