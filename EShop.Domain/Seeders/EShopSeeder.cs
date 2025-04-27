using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;
using EShop.Domain.Repositories;

namespace EShop.Domain.Seeders;

public class EShopSeeder
{
    private readonly DataContext _context;
    public EShopSeeder(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (!_context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product { Name = "Bluza", Ean = "16381711", Price = 69.99M, Stock = 20, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Deleted = false},
                new Product { Name = "Koszulka", Ean = "17811972", Price = 39.99M, Stock = 15, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Deleted = false},
                new Product { Name = "Skiety", Ean = "97607984", Price = 19.99M, Stock = 40, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Deleted = false},
            };
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();
        }
    }
}
