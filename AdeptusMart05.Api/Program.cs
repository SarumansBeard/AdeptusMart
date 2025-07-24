
using AdeptusMart.Business.Services;
using AdeptusMart.BusinessAccessLayer.Services;
using AdeptusMart.DataAccess.Concrete;
using AdeptusMart01.Core.Entities;
using AdeptusMart02.DataAccessLayer.Abstract;
using AdeptusMart02.DataAccessLayer.Concrete;
using AdeptusMart02.DataAccessLayer.Contexts;
using AdeptusMart03.BusinessAccessLayer.Services;
using AdeptusMart05.Api.Context;
using Microsoft.EntityFrameworkCore;

namespace AdeptusMart05.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AdeptusMartDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("AdeptusMart02.DataAccessLayer")
    )
);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowWebUI",
                    builder => builder.WithOrigins("https://localhost:7143")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            



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
            builder.Services.AddScoped<HomeApiContext>();
            builder.Services.AddScoped<ProductApiContext>();
            builder.Services.AddScoped<ShopGridApiContext>();
            




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
               
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseCors("AllowWebUI");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
                       
            app.Run();
        }
    }
}
