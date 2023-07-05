using System.Text.Json;
using MediatR;
using SocialMediaClean.Application.Common.Enums;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.Users.Queries.GetAllUsers;

public class AllUsersHandler : IRequestHandler<AllUsersRecord, Result>
{

    private readonly ISocialMediaCleanContext _context;

    public AllUsersHandler(ISocialMediaCleanContext context)
    {
        _context = context;
    }


    public Task<Result> Handle(AllUsersRecord request, CancellationToken cancellationToken)
    {
        
        var users = _context.User.ToList();

        return Task.FromResult<Result>(new Result()
        {
            JsonData = JsonSerializer.Serialize(users),
            ResultCode = ResultCodeEnum.Success
        });

    }
}

