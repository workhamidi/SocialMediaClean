using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaClean.Application.Common.Enums;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Domain.Entities;

namespace SocialMediaClean.Application.FriendRequest.Commands.AcceptFriendRequest;

public class AcceptFriendRequestHandler : IRequestHandler<AcceptFriendRequestRecord, Result>
{

    private readonly ICurrentUserService _currentUserService;
    private readonly ISocialMediaCleanContext _context;

    public AcceptFriendRequestHandler(
        ICurrentUserService currentUserService,
        ISocialMediaCleanContext context
    )
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task<Result> Handle(AcceptFriendRequestRecord request, CancellationToken cancellationToken)
    {
        var friendRequestToMe = _context.FriendRequest
             .FirstOrDefault(fr =>
                 fr.ReceiverFriendRequestUserId == _currentUserService.UserId &&
                 fr.SenderFriendRequestUserNavigation.UserName == request.UsernameFriendRequestSender
                       );

        if (friendRequestToMe == null)
            return new Result()
            {
                ResultCode = ResultCodeEnum.Failed,
                Description = $"there are no friend request"
            };

        friendRequestToMe.ReceiverIsSeenRequest = true;
        friendRequestToMe.ReceiverIsAcceptedRequest = true;



        var senderFriendRequestUser = _context.User.FirstOrDefault(u => u.UserName == request.UsernameFriendRequestSender);
        var receiverFriendRequestUser = _context.User.FirstOrDefault(u => u.Id == _currentUserService.UserId);



        var newSenderFriendRequestUserContact = new Contacts()
        {
            ContactUserNavigation = receiverFriendRequestUser!,
            OwnerContactsUserNavigation = senderFriendRequestUser!
        };

        _context.Contacts.Add(newSenderFriendRequestUserContact);
        

        await _context.SaveChangesAsync(cancellationToken);

        return new Result()
        {
            Description = $"the {request.UsernameFriendRequestSender} friend request accepted and add to contact"
        };


    }
}

