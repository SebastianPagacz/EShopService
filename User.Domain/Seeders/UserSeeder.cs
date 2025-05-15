using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Domain.Models;
using User.Domain.Repository;

namespace User.Domain.Seeders;

public class UserSeeder(DataContext context) : IUserSeeder
{
    public async Task SeedAsync()
    {
        if (!context.Roles.Any())
        {
            List<Role> roles = new List<Role>
            {
            new Role { Id = 1, Name = "Admin"},
            new Role { Id = 2, Name = "Client"},
            new Role { Id = 3, Name = "Employee"},
            };

            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }

        if (!context.Users.Any())
        {
            List<Models.User> users = new List<Models.User>
            {
                new Models.User { Id = 1, Username = "maciek13", Password = "okon", Roles = new List<Role> { context.Roles.First(r => r.Name == "Admin") } },
                new Models.User { Id = 2, Username = "gosciu", Password = "rybka", Roles = new List<Role> { context.Roles.First(r => r.Name == "Employee") } },
                new Models.User { Id = 3, Username = "peter", Password = "parker", Roles = new List<Role> { context.Roles.First(r => r.Name == "Client") } },
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}
