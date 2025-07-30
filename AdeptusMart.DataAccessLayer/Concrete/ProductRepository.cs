using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdeptusMart01.Core.Entities;
using AdeptusMart.DataAccess.Concrete;
using AdeptusMart02.DataAccessLayer.Abstract;
using AdeptusMart02.DataAccessLayer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AdeptusMart02.DataAccessLayer.Concrete
{
    using AdeptusMart01.Core.Entities;
    using AdeptusMart02.DataAccessLayer.Abstract;
    using AdeptusMart02.DataAccessLayer.Contexts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        private readonly AdeptusMartDbContext _context;

        public ProductRepository(AdeptusMartDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Product> GetByIdWithCategoryAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted && p.Id == id)
                .FirstOrDefaultAsync();
        }


        public async Task<List<Product>> GetFilteredProductsWithCategoryAsync(
        List<Guid> categoryIds, List<decimal>? filteredPrices, decimal? sortBy, string? reset = null)
        {
            // Fiyat aralığı belirleme
            decimal? priceMin = (filteredPrices?.Count > 0) ? filteredPrices[0] : null;
            decimal? priceMax = (filteredPrices?.Count > 1 && filteredPrices[1] != 0) ? filteredPrices[1] : null;

            // Sıralama uygulanmamış temel sorgu
            var query = _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .AsQueryable();

            if (reset == "true")
            {
                categoryIds = null;
                filteredPrices = null;
                sortBy = 11;                
            }

            // Sıralama işlemi
            switch (sortBy)
            {
                case 1:
                    query = query.OrderBy(p => p.Price);
                    break;
                case 2:
                    query = query.OrderByDescending(p => p.Price);
                    break;
                case 4:
                    query = query.OrderBy(p => p.Name);
                    break;
                case 5:
                    query = query.OrderByDescending(p => p.Name);
                    break;
                case 7:
                    query = query.OrderBy(p => p.Star);
                    break;
                case 8:
                    query = query.OrderByDescending(p => p.Star);
                    break;
                case 10:
                    query = query.OrderByDescending(p => p.RegisterTime);
                    break;
                case 11:
                    query = query.OrderBy(p => p.RegisterTime);
                    break;
                default:
                    // Sıralama yapılmaz (varsayılan)
                    break;
            }

            // Kategori filtresi
            if (categoryIds != null && categoryIds.Any())
            {
                query = query.Where(p => categoryIds.Contains(p.CategoryId));
            }

            // Fiyat filtresi
            if (priceMin.HasValue)
            {
                query = query.Where(p => p.Price >= priceMin.Value);
            }
            if (priceMax.HasValue)
            {
                query = query.Where(p => p.Price <= priceMax.Value);
            }


           


            return await query.ToListAsync();
        }

        public async Task<string> GetProductNameById(Guid productId)
        {
            var name =await _context.Products
                .Where(p => p.Id == productId)
                .Select(p=>p.Name)
                .FirstOrDefaultAsync();

            return name.ToString();
        }








    }

}
