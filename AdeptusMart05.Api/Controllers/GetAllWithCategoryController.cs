using AdeptusMart01.Core.Entities;
using AdeptusMart05.Api.Context;
using AdeptusMart05.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace AdeptusMart05.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class GetAllWithCategoryController : ControllerBase
    {
        private readonly ShopGridApiContext _context;
        public GetAllWithCategoryController(ShopGridApiContext context)
        {
            _context = context;
        }

        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<List<Product>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllWithCategory()
        {
            List<Product> product = await _context.GetAllWithCategoryAsync();

            List<ProductDTO> productDTO = new List<ProductDTO>();
            
                for (int i = 0; i < product.Count; i++)
                {
                   var dto = new ProductDTO()
                   {
                       Id = product[i].Id,
                       Name = product[i].Name,
                       Price = product[i].Price,
                       Currency = product[i].Currency,
                       Star = product[i].Star,
                       Details = product[i].Details,
                       Information = product[i].Information,
                       ImageUrl1 = product[i].ImageUrl1,
                       Category = new CategoryDTO
                       {
                           Id = product[i].CategoryId,
                           Name = product[i].Category.Name,
                           ImageUrl = product[i].Category.ImageUrl
                       }
                   };
                   productDTO.Add(dto);
                 }                         
                         

            if (productDTO == null)
            {
                return NotFound("No banner lefts found.");
            }
            if (productDTO.Count == 0)
            {
                return NoContent();
            }
            return Ok(productDTO);
        }
    }
}
