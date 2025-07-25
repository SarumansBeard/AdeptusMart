using AdeptusMart01.Core.Entities;
using AdeptusMart05.Api.Context;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart04.Api.Controllers
{
    [Route("api/itemcountbycategory")]
    [ApiController]
    public class ItemCountByCategoryController : Controller
    {              
        private readonly HomeApiContext _context;
        public ItemCountByCategoryController(HomeApiContext context)
        {
            _context = context;
        }
        [HttpGet("get/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<List<BannerLeft>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            List<Product> products = await _context.GetProductsAsync();

            if (products == null)
            {
                return NotFound("No banner lefts found.");
            }
            if (products.Count == 0)
            {
                return NoContent();
            }

            int productcount = products.Count(p => p.CategoryId == id);

            return Ok(productcount);
        }
        
    }
}
