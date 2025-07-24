using AdeptusMart01.Core.Entities;
using AdeptusMart05.Api.Context;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart05.Api.Controllers
{
    [Route("api/randomTrendingProducts")]
    [ApiController]
    public class RandomTrendingProductsController : ControllerBase
    {
        private readonly HomeApiContext _context;

        public RandomTrendingProductsController(HomeApiContext context)
        {
            _context = context;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<List<Product>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            List<Product> randomTrendingProducts = await _context.GetRandomTrendingProductsAsync();

            if (randomTrendingProducts == null)
            {
                return NotFound("No banner lefts found.");
            }
            if (randomTrendingProducts.Count == 0)
            {
                return NoContent();
            }
            return Ok(randomTrendingProducts);
        }
    }
}
