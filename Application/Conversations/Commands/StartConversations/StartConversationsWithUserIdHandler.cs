using System.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaClean.Application.Common.Enums;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Domain.Entities;

namespace SocialMediaClean.Application.Conversations.Commands.StartConversations;

public class StartConversationsWithUserIdHandler : IRequestHandler<StartConversationsWithUsernameRecord, Result>
{
    private readonly ISocialMediaCleanContext _context;
    private readonly ICurrentUserService _currentUserService;

    public StartConversationsWithUserIdHandler(
        ISocialMediaCleanContext context,
        ICurrentUserService currentUserService
        )
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(StartConversationsWithUsernameRecord request, CancellationToken cancellationToken)
    {

        if (string.IsNullOrEmpty(_currentUserService.UserId))
            return new Result()
            {
                Description = $"somethings is wrong, the user id is null, please try later again"
            };

        var creatorUser = await _context.User.FirstOrDefaultAsync(u => u.Id == _currentUserService.UserId, cancellationToken: cancellationToken);

        if (creatorUser == null)
            return new Result()
            {
                ResultCode = ResultCodeEnum.Failed,
                Description = $"the creator user not found with id {_currentUserService.UserId}"
            };

        var participantUser = await _context.User.FirstOrDefaultAsync(u => u.UserName == request.ParticipantUsername, cancellationToken: cancellationToken);


        if (participantUser == null)
            return new Result()
            {
                ResultCode = ResultCodeEnum.Failed,
                Description = $"the participant user not found with name {request.ParticipantUsername}"
            };



        Domain.Entities.
            Conversations conversation = new Domain.Entities.
                Conversations();

        try
        {
            
            conversation.CreatorUserNavigation = creatorUser;
            conversation.FirstParticipantsUserNavigation = creatorUser;
            conversation.SecondParticipantsUserNavigation= participantUser;

            
            conversation.MessageNavigation = new List<Domain.Entities.Message>();


            var firstMessage = new Domain.Entities.Message
            {
                MessageText = request.Message,
                MessageSenderUserNavigation = creatorUser,
                ConversationsNavigation = conversation,
                MessageSenderUserId = _currentUserService.UserId,
                IsSeen = false
            };


            conversation.MessageNavigation.Add(firstMessage);


            _context.Conversations.Add(conversation);
            

            await _context.SaveChangesAsync(cancellationToken);

        }
        catch (Exception ex)
        {


            var error = new Dictionary<string, string>
            {
                { "0", "Error occurred: " + ex.InnerException }
            };

            return new Result()
            {
                Errors = error,
                ResultCode = ResultCodeEnum.Error,
                Description = $"start conversation was successfully with conversation id {conversation.Id}"
            };
        }



        return new Result()
        {
            ResultCode = ResultCodeEnum.Success,
            Description = $"start conversation was successfully with conversation id {conversation.Id}"
        };

    }
}

