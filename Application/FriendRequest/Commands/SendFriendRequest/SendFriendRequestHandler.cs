using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaClean.Application.Common.Enums;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.FriendRequest.Commands.SendFriendRequest;

public class SendFriendRequestHandler : IRequestHandler<SendFriendRequestRecord, Result>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ISocialMediaCleanContext _context;

    public SendFriendRequestHandler(
        ICurrentUserService currentUserService,
        ISocialMediaCleanContext context
    )
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task<Result> Handle(SendFriendRequestRecord request, CancellationToken cancellationToken)
    {
        var senderUser = await _context.User.FirstOrDefaultAsync(u => u.Id == _currentUserService.UserId, cancellationToken: cancellationToken);

        if (senderUser == null)
            return new Result()
            {
                ResultCode = ResultCodeEnum.Failed,
                Description = $"the creator user not found with id {_currentUserService.UserId}"
            };


        var targetUser = await _context.User.FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken: cancellationToken);

        if (targetUser == null)
            return new Result()
            {
                ResultCode = ResultCodeEnum.Failed,
                Description = $"the target user not found with username {request.Username}"
            };

        var friendRequestDuplicate = _context.FriendRequest.FirstOrDefault(fr =>
             fr.SenderFriendRequestUserId == senderUser.Id &&
             fr.ReceiverFriendRequestUserId == targetUser.Id);

        if (friendRequestDuplicate != null)
            return new Result()
            {
                Description = $"you sent this request to {targetUser.UserName}"
            };

        var friendRequest = new Domain.Entities.FriendRequest()
        {
            ReceiverFriendRequestUserNavigation = targetUser,
            SenderFriendRequestUserNavigation = senderUser,
            ReceiverIsAcceptedRequest = false,
            ReceiverIsSeenRequest = false
        };

        _context.FriendRequest.Add(friendRequest);


        await _context.SaveChangesAsync(cancellationToken);


        return new Result()
        {
            ResultCode = ResultCodeEnum.Success,
            Description = "the friend request sent successfully"
        };
    }
}

