using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Exceptions;
using User.Domain.Models;

namespace User.Application.Services;

public class LoginService : ILoginService
{
    private static List<UserDTO> _loginData = new List<UserDTO>
    {
        new UserDTO {Id = 1, Username = "maciek13", Roles = new List<string> { "admin", "user" } }
    };
    public UserDTO Login(LoginRequest data)
    {
        if (data is null)
            throw new InvalidCredentialException();

        if (data.Username == "maciek13" && data.Password == "okon")
            return _loginData.First(u => u.Username == data.Username);

        throw new InvalidCredentialException();
    }
}
