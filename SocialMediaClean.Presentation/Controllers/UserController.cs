using Microsoft.AspNetCore.Mvc;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Users.Commands.CreateUser;
using SocialMediaClean.Application.Users.Commands.LoginUser;
using SocialMediaClean.Application.Users.Commands.SignOutUser;
using SocialMediaClean.Application.Users.Queries.GetAllUsers;
using SocialMediaClean.Application.Users.Queries.SearchUser;

namespace SocialMediaClean.Presentation.Controllers;

public class UserController : ApiControllerBase
{


    [HttpPost("RegisterUser")]
    public async Task<Result> RegisterUserAsync([FromBody] CreateUserCommandRecord newUser)
    {
        return await Mediator.Send(newUser);
    }


    [HttpPost("Login")]
    public async Task<Result> LoginAsync([FromBody] LogInUserRecord record)
    {
        return await Mediator.Send(record);
    }


    [HttpGet("SignOut")]
    public async Task<Result> SignOutAsync()
    {
        var aa = User.Identity.IsAuthenticated;


        var awaasdvit =await Mediator.Send(new SignOutUserRecord());

        var bb = User.Identity.IsAuthenticated;


        return awaasdvit;

    }


    [HttpGet("SearchUserWithUsername")]
    public async Task<Result> SearchUserWithUsernameAsync([FromQuery] SearchUserRecord user)
    {
        return await Mediator.Send(user);
    }


    [HttpGet("GetAllUser")]
    public async Task<Result> SearchUsersAsync()
    {
        return await Mediator.Send(new AllUsersRecord());
    }



}

