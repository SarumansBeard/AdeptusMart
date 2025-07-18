using AdeptusMart.Business.Services;
using AdeptusMart01.Core.Entities;
using AdeptusMart03.BusinessAccessLayer.Services;
using AdeptusMart04.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdeptusMart04.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartcontext ;
        public CartController(CartService cartcontext)
        {
            _cartcontext = cartcontext;
        }   
        
        public async Task<IActionResult> ShowCartItems(string sessionId)
        {
            var sesionIdfromContext = HttpContext.Session.GetString("SessionId");

            var cartItems = await _cartcontext.ShowCartItems(sesionIdfromContext);

            var model = new CartViewModel
            {
                cartItems = cartItems
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            await _cartcontext.AddToCartService(productId, quantity);          

            return RedirectToAction("Index","Home");
        }







        
    }
}
