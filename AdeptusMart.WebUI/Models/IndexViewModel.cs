using AdeptusMart01.Core.Entities;
using AdeptusMart05.WebUI.DTOs;

namespace AdeptusMart04.WebUI.Models
{
    public class IndexViewModel
    {
        

        public List<BannerLeft> BannerLefts { get; set; } = new List<BannerLeft>();
        public BannerRight BannerRight { get; set; } = new BannerRight(); 
        public List<ProductDTO> ProductDTOs { get; set; } = new List<ProductDTO>(); 
        public List<Category> Categories { get; set; } = new List<Category>(); 
        public TrendingBanner TrendingBanner { get; set; } = new TrendingBanner();
        public List<Product> bestRatedProducts { get; set; }
        public List<Product> trendingProducts { get; set; }
        public List<Product> latestProducts { get; set; }
        public List<CartItem> ShoppingCartItems { get; set; } = new List<CartItem>();





    }
}
