using AdeptusMart01.Core.Entities;
using AdeptusMart05.Api.Context;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart05.Api.Controllers
{
    [Route("api/latestProducts")]
    [ApiController]
    public class LatestProductsController : ControllerBase
    {
        private readonly HomeApiContext _context;

        public LatestProductsController(HomeApiContext context)
        {
            _context = context;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<List<Product>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            List<Product> latestProducts = await _context.GetLatestProductsAsync();

            if (latestProducts == null)
            {
                return NotFound("No banner lefts found.");
            }
            if (latestProducts.Count == 0)
            {
                return NoContent();
            }
            return Ok(latestProducts);
        }
    }
}
