using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EShopService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace EShopService.Tests.Controllers.Tests;

public class CreditCardControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;
    public CreditCardControllerTests(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _client = factory.CreateClient();
        _output = output;
    }
    [Fact]
    public async Task CreditCard_Get_ReturnsOkStatusCode()
    {
        // arrange

        // act
        var response = await _client.GetAsync("/api/creditcard?cardNumber=349779658312797");
        _output.WriteLine(response.ToString());
        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task CreditCard_Get_ReturnsRequestUriTooLongStatusCode()
    {
        // arrange

        // act
        var response = await _client.GetAsync("/api/creditcard?cardNumber=349779658312797123123123");
        _output.WriteLine(response.ToString());
        // assert
        Assert.Equal(HttpStatusCode.RequestUriTooLong, response.StatusCode);
    }

    [Fact]
    public async Task CreditCard_Get_ReturnsBadRequestStatusCode()
    {
        // arrange

        // act
        var response = await _client.GetAsync("/api/creditcard?cardNumber=3497712");
        _output.WriteLine(response.ToString());
        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreditCard_Get_ReturnsNotAcceptableStatusCode()
    {
        // arrange

        // act
        var response = await _client.GetAsync("/api/creditcard?cardNumber=3213abc");
        _output.WriteLine(response.ToString());
        // assert
        Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
    }
}
