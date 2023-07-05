using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.Users.Queries.SearchUser;

public class AllUsersHandler : IRequestHandler<SearchUserRecord, Result>
{

    private readonly ISocialMediaCleanContext _context;

    public AllUsersHandler(ISocialMediaCleanContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(SearchUserRecord request, CancellationToken cancellationToken)
    {
        var user = await _context.User
            .FirstOrDefaultAsync(u => u.UserName.Equals(request.UserName));

        if (user == null)
            return new Result()
            {
                Description = $"user with Username {request.UserName} not found"
            };
        
        return new Result()
        {
            Description = $"the user id with Username {request.UserName} is {user.Id}"
        };
    }
}

