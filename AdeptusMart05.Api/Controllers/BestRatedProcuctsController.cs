using AdeptusMart01.Core.Entities;
using AdeptusMart05.Api.Context;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart05.Api.Controllers
{
    [Route("api/bestRatedProducts")]
    [ApiController]
    public class BestRatedProductsController : ControllerBase
    {
        private readonly HomeApiContext _context;

        public BestRatedProductsController(HomeApiContext context)
        {
            _context = context;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<List<Product>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            List<Product> bestRatedProducts = await _context.GetBestRatedProductsAsync();

            if (bestRatedProducts == null)
            {
                return NotFound("No banner lefts found.");
            }
            if (bestRatedProducts.Count == 0)
            {
                return NoContent();
            }
            return Ok(bestRatedProducts);
        }
    }
}
