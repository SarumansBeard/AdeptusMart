using AdeptusMart.Business.Services;
using AdeptusMart02.DataAccessLayer.Abstract;
using AdeptusMart.DataAccess.Concrete;
using AdeptusMart01.Core;
using Microsoft.EntityFrameworkCore;
using AdeptusMart02.DataAccessLayer.Contexts;
using Microsoft.Extensions.Configuration;
using AdeptusMart02.DataAccessLayer.Concrete;
using AdeptusMart.BusinessAccessLayer.Services;
using AdeptusMart03.BusinessAccessLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext'i connection string ile ekle


builder.Services.AddDbContext<AdeptusMartDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("AdeptusMart02.DataAccessLayer")
    )
);

// Generic repository için kayýt (tek seferlik!)
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// Business servisler için kayýt (tek seferlik!)
builder.Services.AddScoped<HomeService>();


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ShopgridService>();
builder.Services.AddScoped<CartService>();


var app = builder.Build();

// HTTP pipeline ayarlarý
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Statik dosyalar varsa eðer, eðer MapStaticAssets() özel bir extension deðilse kaldýrabilirsin.
// app.MapStaticAssets(); // Eðer özel bir uzantý ise býrakabilirsin

// Endpoint yönlendirmeleri
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
