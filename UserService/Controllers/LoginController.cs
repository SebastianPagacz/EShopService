using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using User.Domain.Models;
using User.Application.Services;
using System.Security.Authentication;
using User.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace UserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IJwtTokenService _tokenService;
    public LoginController(ILoginService loginService, IJwtTokenService tokenService)
    {
        _loginService = loginService;
        _tokenService = tokenService;
    }


    [HttpPost]
    public async Task<IActionResult> Login([FromBody] User.Domain.Models.LoginRequest data)
    {
        try
        {
            var user = await _loginService.LoginAsync(data);

            var token = _tokenService.GenerateToken(user.Id, user.Roles.Select(n => n.Name).ToList());
            return StatusCode(200, token);
        }
        catch (InvalidCredentialsException)
        {
            return StatusCode(404, "invalid data");
        }
        catch (Exception)
        {
            return StatusCode(404, "invalid data");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Get() 
    {
        var users = await _loginService.UsersAsync();
        return StatusCode(200, users);
    }

    //[HttpGet]
    //[Authorize]
    //[Authorize(Policy = "AdminOnly")]
    //public IActionResult AdminPage([FromQuery]string token) 
    //{
    //    return StatusCode(200, "essa");
    //}
}
