using AdeptusMart.BusinessAccessLayer.Services;
using AdeptusMart01.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart05.Api.Context
{
    public class ShopGridApiContext
    {
        private readonly ShopgridService _shopgridService;
        public ShopGridApiContext(ShopgridService shopgridService)
        {
            _shopgridService = shopgridService;
        }
        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            return await _shopgridService.GetAllWithCategoryAsync();
        }
        public async Task<List<Product>> GetFilteredProductsWithCategoryAsync(List<Guid> categoryIds, List<decimal>? filteredPrice, decimal? sortBy, string? reset)
        {
            return await _shopgridService.GetFilteredProductsWithCategoryAsync(categoryIds, filteredPrice, sortBy, reset);
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _shopgridService.GetAllCategoriesAsync();
        }
    }
}
