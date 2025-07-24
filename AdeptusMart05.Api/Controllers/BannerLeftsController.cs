using AdeptusMart.Business.Services;
using AdeptusMart01.Core.Entities;
using AdeptusMart05.Api.Context;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart05.Api.Controllers
{
    [Route("api/bannerlefts")]
    [ApiController]
    public class BannerLeftsController : ControllerBase
    {
        private readonly HomeApiContext _context;

        public BannerLeftsController(HomeApiContext context)
        {
            _context = context;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<List<BannerLeft>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            List<BannerLeft> bannerLefts = await _context.GetBannerLeftsAsync();

            if (bannerLefts == null)
            {
                return NotFound("No banner lefts found.");
            }
            if (bannerLefts.Count == 0)
            {
                return NoContent();
            }
            return Ok(bannerLefts);
        }
    }
}
