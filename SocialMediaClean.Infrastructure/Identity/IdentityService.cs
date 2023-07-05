using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Domain.Entities;

namespace SocialMediaClean.Infrastructure.Identity;



/*
 * Microsoft.AspNetCore.ApiAuthorization.IdentityServeR
 * Microsoft.AspNetCore.Identity.EntityFrameworkCore
 */
public class IdentityService : IIdentityService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;

    public IdentityService(
        UserManager<User> userManager,
        IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
        SignInManager<User> signInManager,
        IAuthorizationService authorizationService,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _signInManager = signInManager;
        _authorizationService = authorizationService;
    }

    public async Task<(IdentityResult, string?)> CreateUserAsync(User newUser, string password, string role)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u =>
            u.Id == newUser.Id ||
            u.UserName == newUser.UserName
            );

        if (user is not null) return (IdentityResult.Failed(
            new IdentityError()
            {
                Description = $"User already exists with id {user.Id}"
            }
        ), user.Id);


        var userResult = await _userManager.CreateAsync(newUser, password);

        var roleResult = await _userManager.AddToRoleAsync(newUser, role);

        if (roleResult.Succeeded is false)
            return (IdentityResult.Failed(
                new IdentityError()
                {
                    Description = $"cannot add role to user with user id :{newUser!.Id}"
                }
            ), newUser.Id);

        return (userResult, newUser.Id);

    }

    public async Task<(IdentityResult, string?, string?)> LoginWithUsernameAsync(string username, string password)
    {

        var findUser = await _userManager.Users.
            FirstOrDefaultAsync(u => u.UserName == username);


        if (findUser is null)
            return (IdentityResult.Failed(
               new IdentityError()
               {
                   Description = $"User not found with name {username}"
               }
           ), null, null);


        var loginResult = await _signInManager.PasswordSignInAsync(findUser, password,
            isPersistent: true, lockoutOnFailure: false);

        if (loginResult.Succeeded is false)
            return (IdentityResult.Failed(
                new IdentityError()
                {
                    Description = $"RequiresTwoFactor : {loginResult.RequiresTwoFactor}" +
                                  $"IsLockedOut : {loginResult.IsLockedOut}" +
                                  $"IsNotAllowed : {loginResult.IsNotAllowed}"

                }
            ), findUser.Id, null);

        var jwtSecurity = GenerateJwtSecurityToken(findUser);

        var token = GetToken(jwtSecurity);

        return (IdentityResult.Success, findUser.Id, token);

    }

    public async Task<(IdentityResult, string?)> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
            return (IdentityResult.Failed(
                new IdentityError()
                {
                    Description = $"User not found with id {userId}"
                }
            ), userId);

        var res = await _userManager.IsInRoleAsync(user, role);

        if (res is false)
        {
            return (IdentityResult.Failed(
                new IdentityError()
                {
                    Description = "something is wrong"
                }
            ), user.Id);
        }

        return (IdentityResult.Success, user.Id);
    }

    public async Task<(IdentityResult, string?)> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return (IdentityResult.Failed(
                new IdentityError()
                {
                    Description = $"User not found with UserId {userId}"
                }
            ), userId);
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);


        if (result.Succeeded is false)
        {
            return (IdentityResult.Failed(result?.Failure?
                .FailureReasons.Select(reason => new IdentityError
                {
                    Description = reason.Message
                }).ToArray()), userId);
        }

        return (IdentityResult.Success, userId);
    }

    public async Task LogOutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }

    private JwtSecurityToken GenerateJwtSecurityToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

        return new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(
                double.Parse(_configuration["JWT:ExpireInMinute"] is null ? "20"
                    : _configuration["JWT:ExpireInMinute"]!)),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

    }

    private string GetToken(JwtSecurityToken jwtToken)
    {
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    private JwtSecurityToken ExpireJwtToken(string tokenStr)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.ReadJwtToken(tokenStr);

        return new JwtSecurityToken(
            issuer: null,
            audience: null,
            expires: DateTime.Now.AddMinutes(-1),
            claims: null,
            signingCredentials: null
        );
    }

}

