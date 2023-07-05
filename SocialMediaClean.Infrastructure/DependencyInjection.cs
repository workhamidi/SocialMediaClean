using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Models.Identity;
using SocialMediaClean.Domain.Entities;
using SocialMediaClean.Infrastructure.Identity;
using SocialMediaClean.Infrastructure.Persistence.Context;

namespace SocialMediaClean.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ISocialMediaCleanContext, SocialMediaCleanContext>(options =>
                options.UseInMemoryDatabase("StateSetInMemory"));
        }
        else
        {
            services.AddDbContext<SocialMediaCleanContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        services.AddScoped<ISocialMediaCleanContext>(provider => provider.GetRequiredService<SocialMediaCleanContext>());

        services
            .AddDefaultIdentity<User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 1;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                }
            )
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<SocialMediaCleanContext>();


        services.AddIdentityServer()
            .AddApiAuthorization<User, SocialMediaCleanContext>();



        services.AddTransient<IIdentityService, IdentityService>();


        //services.AddAuthentication()
        //    // این خودش پیشفرض توی cookie‌ اطلاعات لازم رو اضافه می کنه
        //    /*.AddIdentityServerJwt()*/;

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
                };
            });




        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.SearchUser, policy => policy.RequireRole(Roles.User.Name));
            options.AddPolicy(Policies.StartConversation, policy => policy.RequireRole(Roles.User.Name));
            options.AddPolicy(Policies.SendMassage, policy => policy.RequireRole(Roles.User.Name));
            options.AddPolicy(Policies.SendFriendRequest, policy => policy.RequireRole(Roles.User.Name));
            options.AddPolicy(Policies.GetNewMessages, policy => policy.RequireRole(Roles.User.Name));
            options.AddPolicy(Policies.GetFriendRequests, policy => policy.RequireRole(Roles.User.Name));
            options.AddPolicy(Policies.GetContacts, policy => policy.RequireRole(Roles.User.Name));
            options.AddPolicy(Policies.EditMessage, policy => policy.RequireRole(Roles.User.Name));
        });



        services.AddSingleton<IRegisterRolePermissionToDatabase,
            RegisterRolePermissionToDatabase>();


        return services;
    }
}

