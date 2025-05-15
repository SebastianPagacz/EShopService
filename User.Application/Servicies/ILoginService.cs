using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Models;

namespace User.Application.Services;

public interface ILoginService
{
	Task<UserDTO> LoginAsync(LoginRequest data);

	Task<List<Domain.Models.User>> UsersAsync();
}
