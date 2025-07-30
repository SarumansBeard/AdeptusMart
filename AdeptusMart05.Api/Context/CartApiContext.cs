using AdeptusMart01.Core.Entities;
using AdeptusMart03.BusinessAccessLayer.Services;

namespace AdeptusMart04.Api.Context
{
    public class CartApiContext
    {
        private readonly CartService _cartService;
        public CartApiContext(CartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<List<CartItem>> GetCartItemsAsync(string userId)
        {
            return await _cartService.ShowCartItems(userId);
        }

        public async Task AddToCartAsync(Guid productId, int quantity, string userId)
        {
            await _cartService.AddToCartService(productId, quantity, userId);
        }
    }
}
