using SocialMediaClean.Application.Common.Models.Dtos;
using SocialMediaClean.Application.Users.Commands.CreateUser;
using SocialMediaClean.Domain.Entities;

namespace SocialMediaClean.Application.Common.Mapping;

public static class MessageMapping
{
    public static MessageDto MessageToMessageDto(this Domain.Entities.Message message)
    {
        return new MessageDto()
        {
            IsSeen = message.IsSeen,
            ConversationsId = message.ConversationsId,
            MessageText = message.MessageText,
            MessageSenderUserId = message.MessageSenderUserId
        };
    }
}

