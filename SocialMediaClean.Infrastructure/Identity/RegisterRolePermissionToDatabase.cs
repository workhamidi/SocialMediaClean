using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Models.Identity;

namespace SocialMediaClean.Infrastructure.Identity;

public class RegisterRolePermissionToDatabase : IRegisterRolePermissionToDatabase
{
    private readonly IServiceProvider _services;

    public RegisterRolePermissionToDatabase(
        IServiceProvider services)
    {
        _services = services;
    }

    public async Task Register()
    {
        await RegisterRoles();
    }

    private async Task RegisterRoles()
    {
        var roles = Roles.List();

        using (var scope = _services.CreateScope())
        {

            var roleManager =
                scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();


            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

        }
    }



}

