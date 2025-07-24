using AdeptusMart01.Core.Entities;
using AdeptusMart05.Api.Context;
using AdeptusMart05.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart05.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductApiContext _context;
        public ProductController(ProductApiContext context)
        {
            _context = context;
        }

        [HttpGet("get/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        [ProducesResponseType<List<Product>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductById(Guid Id)
        {
            Product product = await _context.GetProductByIdAsync(Id);

            ProductDTO productDTO = new ProductDTO()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Currency = product.Currency,
                Star = product.Star,
                Details = product.Details,
                Information = product.Information,
                ImageUrl1 = product.ImageUrl1,
                Category = new CategoryDTO
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,                    
                    ImageUrl = product.Category.ImageUrl
                },
            };

            if (product == null)
            {
                return NotFound("No banner lefts found.");
            }           
            return Ok(productDTO);
        }
    }
}
