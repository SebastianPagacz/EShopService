using Microsoft.AspNetCore.Mvc;
using EShop.Domain.Repositories.Services;
using EShop.Domain.Repositories.Interfaces;
using EShop.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;
    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    // GET: api/<ProductController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _repository.GetAllAsync();
        return StatusCode(200, products);
    }

    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        return StatusCode(200, product);
    }

    // POST api/<ProductController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        await _repository.AddAsync(product);
        return StatusCode(200, product);
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Product product)
    {
        await _repository.UpdateAsync(product);
        return StatusCode(200, product);
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        product.Deleted = true;
        var result = await _repository.UpdateAsync(product);
        return StatusCode(200, product);
    }
}
