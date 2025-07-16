using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AdeptusMart.BusinessAccessLayer.Services
{
    public class ShopgridService
    {
        private readonly IProductRepository _productRepo;
        private readonly IRepository<Category> _categoryRepo;

        public ShopgridService(IProductRepository productRepo , IRepository<Category> categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }
        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            var products = await _productRepo.GetAllWithCategoryAsync();
            

            return products;
        }


        public async Task<List<Product>> GetFilteredProductsWithCategoryAsync(List<Guid> categoryIds, List<decimal>? filteredPrice,decimal? sortBy, string? reset)
        {
            return await _productRepo.GetFilteredProductsWithCategoryAsync(categoryIds,filteredPrice,sortBy,reset);            
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var categoriesAll = await _categoryRepo.GetAllAsync();

            return categoriesAll.ToList();
        }



        


    }
}
