using AdeptusMart01.Core.Entities;
using AdeptusMart05.Api.Context;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart05.Api.Controllers
{
    [Route("api/bannerRight")]
    [ApiController]
    public class BannerRightController : ControllerBase
    {
        private readonly HomeApiContext _context;

        public BannerRightController(HomeApiContext context)
        {
            _context = context;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<BannerRight>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            BannerRight bannerRight = await _context.GetBannerRightAsync();

            if (bannerRight == null)
            {
                return NotFound("No banner lefts found.");
            }            
            return Ok(bannerRight);
        }
    }
}
