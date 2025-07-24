using AdeptusMart.BusinessAccessLayer.Services;
using AdeptusMart01.Core.Entities;
using AdeptusMart05.Api.Context;
using AdeptusMart05.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AdeptusMart05.Api.Controllers
{
    [Route("api/productfiltered")]
    [ApiController]
    public class GetFilteredProductsWithCategory : Controller
    {
        private readonly ShopGridApiContext _context;
        public GetFilteredProductsWithCategory(ShopGridApiContext context)
        {
            _context = context;
        }

        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<List<Product>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductByFilter([FromQuery]List<Guid> categoryIds, [FromQuery] List<decimal>? filteredPrice, [FromQuery] decimal? sortBy, [FromQuery] string? reset)
        {
            List<Product> product = await _context.GetFilteredProductsWithCategoryAsync(categoryIds, filteredPrice, sortBy, reset);

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

            if (product == null)
            {
                return NotFound("No banner lefts found.");
            }
            return Ok(productDTO);
        }


    }
}
