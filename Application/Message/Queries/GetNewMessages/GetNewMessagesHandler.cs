using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Mapping;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.Message.Queries.GetNewMessages;

public class GetNewMessagesHandler : IRequestHandler<GetNewMessagesRecord, Result>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ISocialMediaCleanContext _context;

    public GetNewMessagesHandler(
        ICurrentUserService currentUserService,
        ISocialMediaCleanContext context
        )
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task<Result> Handle(GetNewMessagesRecord request, CancellationToken cancellationToken)
    {

        var messages = _context.Conversations.Where(u =>
                  u.FirstParticipantsUserId == _currentUserService.UserId ||
                  u.SecondParticipantsUserId == _currentUserService.UserId)
             .Select(c => c.MessageNavigation)
             .SelectMany(mList => mList!.Where(m => m.IsSeen == false))
             .ToList();

        if (messages.Count == 0)
            return new Result()
            {
                Description = "there are no new messages"
            };



        /*
         * System.Text.Json.JsonException: A possible object cycle was detected.
         * This can either be due to a cycle or if the object depth is larger than 
         * the maximum allowed depth of 64.Consider using ReferenceHandler.Preserve 
         * on JsonSerializerOptions to support cycles. Path: $.MessageSenderUserNavigation.
         * MessageNavigation.MessageSenderUserNavigation.MessageNavigation.
         */

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };


        var jsonMessages = JsonSerializer.Serialize(
            messages.Select(m=>m.MessageToMessageDto()), options);

        
        messages.ForEach(message => message.IsSeen = true);


        await _context.SaveChangesAsync(cancellationToken);

        return new Result()
        {
            Description = "messages successfully fetched",
            JsonData = jsonMessages
        };


    }
}

