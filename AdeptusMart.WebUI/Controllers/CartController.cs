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
        
        public async Task<IActionResult> ShowCartItems()
        {
            var sesionIdfromContext = HttpContext.Session.GetString("SessionId");

            List<CartItem> cartItems = await _cartcontext.ShowCartItems(sesionIdfromContext);

            var model = new CartViewModel();

            model.CartItems = cartItems;

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(CartViewModel model)
        {

            Guid productId = model.CartItem.ProductId;
            int quantity = model.CartItem.Quantity;

            var sesionIdfromContext = HttpContext.Session.GetString("SessionId");

            await _cartcontext.AddToCartService(productId, quantity, sesionIdfromContext);          

            return RedirectToAction("Index","Home");
        }







        
    }
}
