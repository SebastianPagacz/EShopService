using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Models;
using EShop.Domain.Repositories;

namespace EShop.Domain.Seeders;

public class EShopSeeder(DataContext _context) : IEShopSeeder
{
    public async Task SeedAsync()
    {
        if (!_context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product { Name = "Cobi", Ean = "1234" },
                new Product { Name = "Duplo", Ean = "431" },
                new Product { Name = "Lego", Ean = "12212" }
            };
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();
        }
    }
}
