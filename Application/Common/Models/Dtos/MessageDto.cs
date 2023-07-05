
namespace SocialMediaClean.Application.Common.Models.Dtos;

public class MessageDto
{
    public string MessageSenderUserId { get; set; } = null!;

    public int ConversationsId { get; set; }

    public bool IsSeen { get; set; } = false;

    public string MessageText { get; set; } = null!;
}

