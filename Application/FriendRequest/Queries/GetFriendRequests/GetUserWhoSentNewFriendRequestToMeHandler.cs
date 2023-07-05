using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Mapping;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.FriendRequest.Queries.GetFriendRequests;

public class GetUserWhoSentNewFriendRequestToMeHandler : IRequestHandler<GetUserWhoSentNewFriendRequestToMeRecord, Result>
{

    private readonly ICurrentUserService _currentUserService;
    private readonly ISocialMediaCleanContext _context;

    public GetUserWhoSentNewFriendRequestToMeHandler(
        ICurrentUserService currentUserService,
        ISocialMediaCleanContext context
    )
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task<Result> Handle(GetUserWhoSentNewFriendRequestToMeRecord request, CancellationToken cancellationToken)
    {

        var friendRequests = _context.FriendRequest
            .Include(fr=>fr.SenderFriendRequestUserNavigation)
            .Where(fr => 
                fr.ReceiverIsSeenRequest == false  && 
                fr.ReceiverFriendRequestUserId == _currentUserService.UserId)
            .ToList();


        if (friendRequests.Count == 0)
            return new Result()
            {
                Description = "there are no new friend Requests or don't have friend request"
            };


        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };


        var jsonFriendRequests = JsonSerializer.Serialize(
            friendRequests.Select(fr => fr.FriendRequestToNewFriendRequestDto())
            , options);


        friendRequests.ForEach(fr => fr.ReceiverIsSeenRequest = true);

        
        await _context.SaveChangesAsync(cancellationToken);


        return new Result()
        {
            Description = "messages successfully fetched",
            JsonData = jsonFriendRequests
        };

    }
}

