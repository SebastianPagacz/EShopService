using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using EShop.Domain.Models;
using System.Text.Json;
using Xunit.Abstractions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace EShopService.IntegrationTests.Controllers;

public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _output;
    private readonly HttpClient _client;
    private WebApplicationFactory<Program> _factory;

    public ProductControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services
                    .SingleOrDefault(services => services.ServiceType == typeof(DbContextOptions<DbContext>));

                    services.Remove(dbContextOptions);

                    services.
                        AddDbContext<DbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
                });
            });
        _client = _factory.CreateClient();
        _output = output;
    }


    [Fact]
    public async Task GetProductDetails_ReturnsProductsData()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            dbContext.Products.RemoveRange(dbContext.Products);
            var products = new List<Product>
                {
                    new Product { Name = "Test1", Ean = "1234", Deleted = false },
                    new Product { Name = "Test2", Ean = "4321", Deleted = true },
                    new Product { Name = "Test3", Ean = "1122", Deleted = false }
                };

            dbContext.Products.AddRange(products);
            await dbContext.SaveChangesAsync();
        }

        // act
        var response = await _client.GetAsync("/api/product");
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadAsStringAsync();
        _output.WriteLine(responseData);
        var productList = JsonSerializer.Deserialize<List<Product>>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // assert
        Assert.NotNull(productList);
        Assert.Contains(productList, p => p.Name == "Test1");
    }
}