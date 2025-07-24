using AdeptusMart01.Core.Entities;
using AdeptusMart05.Api.Context;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart05.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly HomeApiContext _context;

        public CategoriesController(HomeApiContext context)
        {
            _context = context;
        }
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<List<Category>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            List<Category> categories = await _context.GetCategoriesAsync();

            if (categories == null)
            {
                return NotFound("No banner lefts found.");
            }
            if (categories.Count == 0)
            {
                return NoContent();
            }
            return Ok(categories);
        }
    }
}
