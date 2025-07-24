using AdeptusMart.Business.Services;
using AdeptusMart.BusinessAccessLayer.Services;
using AdeptusMart.DataAccess.Concrete;
using AdeptusMart02.DataAccessLayer.Abstract;
using AdeptusMart02.DataAccessLayer.Concrete;
using AdeptusMart02.DataAccessLayer.Contexts;
using AdeptusMart03.BusinessAccessLayer.Services;
using Microsoft.EntityFrameworkCore;

namespace AdeptusMart06.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddDbContext<AdeptusMartDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("AdeptusMart02.DataAccessLayer")
    )
);


            builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICartRepository, EfCartRepository>();
            builder.Services.AddScoped<IAccountRepository, EfAccountRepository>();


            builder.Services.AddScoped<HomeService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ShopgridService>();
            builder.Services.AddScoped<CartService>();
            builder.Services.AddScoped<RegisterService>();
            builder.Services.AddScoped<LoginService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {             
               app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Management",
                    areaName: "Management",
                    pattern: "Management/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "productDetails",
                    pattern: "shop-details/{id}",
                    defaults: new { controller = "Product", action = "Details" });

                endpoints.MapControllerRoute(
                    name: "Shopgrid",
                    pattern: "Shopgrid/",
                    defaults: new { controller = "Shopgrid", action = "Index" });


            });

            app.Run();
        }
    }
}
