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
using System.Net;

namespace EShopService.Tests.Controllers.Tests;

public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;
    public ProductControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _client = factory.CreateClient();
        _output = output;
    }
    [Fact]
    public async Task ProductController_Get_ReturnOkStatusCode()
    {
        // arange

        // act
        var resposne = await _client.GetAsync("/api/product");
        _output.WriteLine(resposne.ToString());

        // assert
        Assert.Equal(HttpStatusCode.OK, resposne.StatusCode);
    }

    [Fact]
    public async Task ProductController_GetById_ReturnOkStatusCode()
    {
        // arange

        // act
        var resposne = await _client.GetAsync("/api/product/1");
        _output.WriteLine(resposne.ToString());

        // assert
        Assert.Equal(HttpStatusCode.OK, resposne.StatusCode);
    }
}
