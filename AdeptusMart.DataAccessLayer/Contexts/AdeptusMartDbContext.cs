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
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet <Account> Accounts { get; set; }
        public DbSet <Receipt> Receipts { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=BERTHA\\ANTOV;Database=AdeptusMartDb;User Id=sa;Password=1234;Encrypt=False;");
            }
            base.OnConfiguring(optionsBuilder);
        }





    }
}
