using MediatR;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Users.Commands.CreateUser;

namespace SocialMediaClean.Application.Users.Commands.SignOutUser;

public class SignOutUserHandler : IRequestHandler<SignOutUserRecord, Result>
{

    private readonly IIdentityService _identityService;

    public SignOutUserHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(SignOutUserRecord request, CancellationToken cancellationToken)
    {
        
        await _identityService.LogOutUserAsync();

       return  new Result()
       {
           Description = "SingOut user was successfully"
       };
    }
}

