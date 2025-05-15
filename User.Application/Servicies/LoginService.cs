using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Domain.Exceptions;
using User.Domain.Models;
using User.Domain.Repository;

namespace User.Application.Services;

public class LoginService(DataContext context) : ILoginService
{
    public async Task<UserDTO> LoginAsync(LoginRequest data)
    {
        var user = await context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Username == data.Username);
        
        if (user is null)
            throw new InvalidCredentialException();

        if (user.Password == data.Password)
            return new UserDTO { Id = user.Id, Username = user.Username, Roles = user.Roles };
        
        throw new InvalidCredentialException();
    }

    public async Task<List<Domain.Models.User>> UsersAsync()
    {
        return await context.Users.ToListAsync();
    }
}
