using MediatR;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Mapping;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.Users.Commands.LoginUser;

public class SignOutUserHandler : IRequestHandler<LogInUserRecord, Result>
{

    private readonly IIdentityService _identityService;

    public SignOutUserHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(LogInUserRecord request, CancellationToken cancellationToken)
    {


        var logInResult = await _identityService
            .LoginWithUsernameAsync(request.Username, request.Password);


        if (logInResult.Item1.Succeeded is false)
        {
            return logInResult.Item1.IdentityResultToResult();
        }

        return logInResult.Item1.IdentityResultToResult(
            logInResult.Item3,
            $"LogIn was successfully for user id {logInResult.Item2}",
            logInResult.Item2);


    }
}

