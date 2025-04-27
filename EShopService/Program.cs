using EShop.Domain.Repositories;
using EShop.Application;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Repositories.Interfaces;
using EShop.Domain.Repositories.Services;

namespace EShopService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnextion")));

        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        builder.Services.AddControllers();

        builder.Services.AddScoped<ICardValidator, CardValidator>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
