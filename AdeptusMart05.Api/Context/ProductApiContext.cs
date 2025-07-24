using AdeptusMart01.Core.Entities;
using AdeptusMart03.BusinessAccessLayer.Services;

namespace AdeptusMart05.Api.Context
{
    public class ProductApiContext
    {
        private readonly ProductService _productService;

        public ProductApiContext(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _productService.GetProductByIdAsync(id);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _productService.GetCategoriesAsync();
        }
    }
}
