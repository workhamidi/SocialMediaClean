using MediatR;
using SocialMediaClean.Application.Common.Attributes.Security;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Mapping;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Common.Models.Identity;

namespace SocialMediaClean.Application.Users.Commands.CreateUser;


[Authorize(Roles = $"{nameof(Roles.User)}")]
[Authorize(Policy = $"{nameof(Policies.SearchUser)}")]
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRecord, Result>
{
    private readonly IIdentityService _identityService;

    public CreateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result> Handle(CreateUserCommandRecord userRec, CancellationToken cancellationToken)
    {

        var user = userRec.CreateUserCommandRecordToUser();

        var createResult = await _identityService.CreateUserAsync(
            user,
             userRec.Password,
             Roles.User.Name
         );

        if (createResult.Item1.Succeeded is false)
        {
            return createResult.Item1.IdentityResultToResult();
        }

        var loginResult = await _identityService.LoginWithUsernameAsync(
            user.UserName,
            userRec.Password
        );


        if (loginResult.Item1.Succeeded is false)
        {
            return loginResult.Item1.IdentityResultToResult();
        }

        return createResult.Item1.IdentityResultToResult(
            loginResult.    Item3,
            $"create user was successful and" +
            $" user id is {createResult.Item2}",
            createResult.Item2);
    }
}
