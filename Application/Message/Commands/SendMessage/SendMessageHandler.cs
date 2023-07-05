using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaClean.Application.Common.Enums;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Conversations.Commands.StartConversations;

namespace SocialMediaClean.Application.Message.Commands.SendMessage;

public class SendMessageHandler : IRequestHandler<SendMessage, Result>
{

    private readonly ICurrentUserService _currentUserService;
    private readonly ISocialMediaCleanContext _context;
    private readonly ISender _mediator;

    public SendMessageHandler(
        ICurrentUserService currentUserService,
        ISocialMediaCleanContext context,
        ISender mediator
    )
    {
        _currentUserService = currentUserService;
        _context = context;
        _mediator = mediator;
    }

    public async Task<Result> Handle(SendMessage request, CancellationToken cancellationToken)
    {

        var senderUser = await _context.User.FirstOrDefaultAsync(u => u.Id == _currentUserService.UserId, cancellationToken: cancellationToken);

        if (senderUser == null)
            return new Result()
            {
                ResultCode = ResultCodeEnum.Failed,
                Description = $"the creator user not found with id {_currentUserService.UserId}"
            };

        var conversation =
            await _context.Conversations.FirstOrDefaultAsync(
                c =>
                    (c.FirstParticipantsUserId == _currentUserService.UserId &&
                     c.SecondParticipantsUserNavigation.UserName
                     == request.Username) ||
                    (c.SecondParticipantsUserId == _currentUserService.UserId &&
                     c.FirstParticipantsUserNavigation.UserName
                     == request.Username), cancellationToken: cancellationToken);

        var newConversation = new StartConversationsWithUsernameRecord(
            request.Username, request.Message
        );
        
        if (conversation == null)
            return await _mediator.Send(newConversation, cancellationToken);

        var newMessage = new Domain.Entities.Message()
        {
            ConversationsNavigation = conversation,
            MessageSenderUserNavigation = senderUser,
            MessageSenderUserId = _currentUserService.UserId!,
            MessageText = request.Message,
            IsSeen = false,

        };

        conversation.MessageNavigation ??= new List<Domain.Entities.Message>();
        
        conversation.MessageNavigation.Add(newMessage);


        _context.Message.Add(newMessage);

        await _context.SaveChangesAsync(cancellationToken);

        return new Result()
        {
            ResultCode = ResultCodeEnum.Success,
            Description = "the message sent successfully"
        };

    }
}

