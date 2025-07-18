using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdeptusMart03.BusinessAccessLayer.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IRepository<Category> _categoryRepo;

        public ProductService(IProductRepository productRepo, IRepository<Category> categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepo.GetByIdWithCategoryAsync(id);

            if (product == null)
            {
                return new Product
                {
                    Name = "Başlık Bulunamadı",
                    Price = 0,
                    Currency = "USD",
                    Star = 0,
                    Details = "Detay bulunamadı.",
                    Information = "Bilgi bulunamadı.",
                    ImageUrl1 = "not-found.jpg",
                    CategoryId = Guid.Empty,
                    Category = new Category { Name = "Kategori Yok" }
                };
            }

            return product;
        }

        public async Task<List<Product>> GetRandomProductsExceptAsync(Guid excludedId, int count = 6)
        {
            var products = await _productRepo.GetAllWithCategoryAsync();
            var filteredProducts = products
                .Where(p => p.Id != excludedId)
                .ToList();

            var random = new Random();
            return filteredProducts.OrderBy(x => random.Next()).Take(count).ToList();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _categoryRepo.GetAllAsync();

            return categories.Where(x => !x.IsDeleted).ToList();
        }
    }

}
