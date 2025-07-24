using AdeptusMart01.Core.Entities;

namespace AdeptusMart06.WebUI.Models
{
    public class IndexViewModel
    {
        

        public List<BannerLeft> BannerLefts { get; set; } = new List<BannerLeft>();
        public BannerRight BannerRight { get; set; } = new BannerRight(); 
        public List<Product> Products { get; set; } = new List<Product>(); 
        public List<Category> Categories { get; set; } = new List<Category>(); 
        public TrendingBanner TrendingBanner { get; set; } = new TrendingBanner();
        public List<Product> bestRatedProducts { get; set; }
        public List<Product> trendingProducts { get; set; }
        public List<Product> latestProducts { get; set; }
        public List<CartItem> ShoppingCartItems { get; set; } = new List<CartItem>();





    }
}
