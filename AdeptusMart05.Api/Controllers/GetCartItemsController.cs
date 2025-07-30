using AdeptusMart01.Core.Entities;
using AdeptusMart04.Api.Context;
using AdeptusMart04.Api.DTOs;
using AdeptusMart05.Api.Context;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart04.Api.Controllers
{
    [Route("api/cartitems")]
    [ApiController]
    public class GetCartItems : ControllerBase
    {
        private readonly CartApiContext _context;

        public GetCartItems(CartApiContext context)
        {
            _context = context;
        }
        [HttpGet("get/{userid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<List<CartItem>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCartByUserId(string userId)
        {
            
            var cartItems = await _context.GetCartItemsAsync(userId);
                        

            List<CartItemDTO> cartitemDTOs = new List<CartItemDTO>();
            for (int i = 0; i<cartItems.Count;i++) 
            {
                var dto = new CartItemDTO()
                {
                    Id = cartItems[i].Id,
                    ProductId = cartItems[i].ProductId,
                    ProductName = cartItems[i].ProductName,
                    CartId = cartItems[i].CartId,
                    Quantity = cartItems[i].Quantity
                };
                cartitemDTOs.Add(dto);
            };

            if (cartitemDTOs == null)
            {
                return NotFound("No banner lefts found.");
            }
            if (cartitemDTOs.Count == 0)
            {
                return NoContent();
            }


            return Ok(cartitemDTOs);
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<List<BannerLeft>>(StatusCodes.Status200OK)]
        public async Task AddToCartByAsync([FromQuery]Guid productId, [FromQuery] int quantity, [FromQuery] string userId)
        {
            if (productId == Guid.Empty || quantity <= 0 || userId == null)
            {
                throw new ArgumentException("Invalid input parameters.");
            }
            await _context.AddToCartAsync(productId, quantity, userId);
        }
    }
}
