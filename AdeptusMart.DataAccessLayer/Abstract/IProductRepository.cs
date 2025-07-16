using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdeptusMart01.Core.Entities;

namespace AdeptusMart02.DataAccessLayer.Abstract
{
    using AdeptusMart01.Core.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllWithCategoryAsync();
        Task<Product> GetByIdWithCategoryAsync(Guid id);
        Task<List<Product>> GetFilteredProductsWithCategoryAsync(List<Guid> id , List<decimal>? filteredPrices,decimal? sortBy, string? reset);
       

    
    }

}
