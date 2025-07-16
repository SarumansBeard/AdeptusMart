using AdeptusMart01.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdeptusMart02.DataAccessLayer.Contexts
{
    public class AdeptusMartDbContext :DbContext
    {
        public AdeptusMartDbContext()
        {
            
        }

        public AdeptusMartDbContext(DbContextOptions<AdeptusMartDbContext> options) : base(options)
        {

        }
        

        public DbSet <BannerLeft> BannerLefts { get; set; }
        public DbSet <BannerRight> BannerRights { get; set; }
        public DbSet <Product> Products { get; set; }
        public DbSet <Category> Categories { get; set; }
        public DbSet <TrendingBanner> TrendingBanners { get; set; }
        public DbSet <Cart> Carts { get; set; }
        public DbSet <Account> Accounts { get; set; }
        public DbSet <Receipt> Receipts { get; set; }
        







    }
}
