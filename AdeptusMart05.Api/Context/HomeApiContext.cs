using AdeptusMart.Business.Services;
using AdeptusMart01.Core.Entities;

namespace AdeptusMart05.Api.Context
{
    public class HomeApiContext
    {
        private readonly HomeService _homeService;

        public HomeApiContext(HomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _homeService.GetProductsAsync();
        }

        public async Task<List<BannerLeft>> GetBannerLeftsAsync()
        {
            return await _homeService.GetBannerLeftsAsync();
        }

        public async Task<BannerRight> GetBannerRightAsync()
        {
            return await _homeService.GetBannerRightAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _homeService.GetCategoriesAsync();
        }

        public async Task<TrendingBanner> GetTrendingBannerAsync()
        {
            return await _homeService.GetTrendingBannerAsync();
        }

        public async Task<List<Product>> GetBestRatedProductsAsync()
        {
            return await _homeService.GetBestRatedProductsAsync();
        }

        public async Task<List<Product>> GetRandomTrendingProductsAsync()
        {
            return await _homeService.GetRandomTrendingProductsAsync();
        }

        public async Task<List<Product>> GetLatestProductsAsync()
        {
            return await _homeService.GetLatestProductsAsync();
        }
    }
}
