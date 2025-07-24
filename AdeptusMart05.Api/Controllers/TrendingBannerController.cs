using AdeptusMart01.Core.Entities;
using AdeptusMart05.Api.Context;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart05.Api.Controllers
{
    [Route("api/trendingBanner")]
    [ApiController]
    public class TrendingBannerController : ControllerBase
    {
        private readonly HomeApiContext _context;

        public TrendingBannerController(HomeApiContext context)
        {
            _context = context;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<TrendingBanner>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            TrendingBanner trendingBanner = await _context.GetTrendingBannerAsync();

            if (trendingBanner == null)
            {
                return NotFound("No banner lefts found.");
            }            
            return Ok(trendingBanner);
        }
    }
}
