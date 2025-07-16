using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdeptusMart.Business.Services
{
    public class HomeService
    {
        private readonly IRepository<BannerLeft> _bannerLeftRepo;
        private readonly IRepository<BannerRight> _bannerRightRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<TrendingBanner> _trendingBannerRepo;

        public HomeService(
            IRepository<BannerLeft> bannerLeftRepo,
            IRepository<BannerRight> bannerRightRepo,
            IRepository<Product> productRepo,
            IRepository<Category> categoryRepo,
            IRepository<TrendingBanner> trendingBannerRepo)
        {
            _bannerLeftRepo = bannerLeftRepo;
            _bannerRightRepo = bannerRightRepo;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _trendingBannerRepo = trendingBannerRepo;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = (await _productRepo.GetAllAsync())
                .Where(p => !p.IsDeleted)
                .ToList();

            return products;
        }


        public async Task<List<BannerLeft>> GetBannerLeftsAsync()
        {
            var list = (await _bannerLeftRepo.GetAllAsync())
                .Where(b => !b.IsDeleted)
                .ToList();

            if (list == null || !list.Any())
            {
                list = new List<BannerLeft>
                {
                    new BannerLeft
                    {
                        MainTitle = "Başlık Bulunamadı",
                        MainTitleColorCode = "#000000",
                        Title = "Başlık Bulunamadı",
                        TitleColorCode = "#000000",
                        ButtonText = "Buton Bulunamadı",
                        ButtonTextColorCode = "#000000",
                    }
                };
            }

            return list;
        }

        public async Task<BannerRight> GetBannerRightAsync()
        {
            var banner = (await _bannerRightRepo.GetAllAsync())
                .Where(b => !b.IsDeleted)
                .OrderByDescending(b => b.Id)
                .FirstOrDefault();

            if (banner == null)
            {
                banner = new BannerRight
                {
                    MainTitle = "Başlık Bulunamadı",
                    MainTitleColorCode = "#000000",
                    ButtonText = "Buton Bulunamadı",
                    ButtonTextColorCode = "#000000",
                    GoShopButtonText = "Buton Bulunamadı",
                    GoShopButtonTextColorCode = "#000000"
                };
            }

            return banner;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var list = (await _categoryRepo.GetAllAsync())
                .Where(c => !c.IsDeleted)
                .ToList();
            return list;
        }

        public async Task<TrendingBanner> GetTrendingBannerAsync()
        {
            var banner = (await _trendingBannerRepo.GetAllAsync())
                .Where(b => !b.IsDeleted)
                .OrderByDescending(b => b.Id)
                .FirstOrDefault();

            if (banner == null)
            {
                banner = new TrendingBanner
                {
                    MainTitle = "Başlık Bulunamadı",
                    MainTitleColorCode = "#000000",
                    GoShopButtonText = "Başlık Bulunamadı",
                    GoShopButtonTextColorCode = "#000000",
                    BackGroundUrl = ""
                };
            }

            return banner;
        }

        public async Task<List<Product>> GetBestRatedProductsAsync()
        {
            var products = (await _productRepo.GetAllAsync())
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.Star)  // rating alanı varsa
                .Take(6)
                .ToList();

            return products;
        }

        public async Task<List<Product>> GetRandomTrendingProductsAsync()
        {
            var products = (await _productRepo.GetAllAsync())
                .Where(x=> !x.IsDeleted)
                .OrderBy(_ => Guid.NewGuid())  // rastgele sıralama için
                .Take(6)
                .ToList();

            return products;
        }

        public async Task<List<Product>> GetLatestProductsAsync()
        {
            var products = (await _productRepo.GetAllAsync())
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.RegisterTime)  // oluşturulma tarihi varsa
                .Take(6)
                .ToList();

            return products;
        }

    }

}