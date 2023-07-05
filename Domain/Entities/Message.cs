namespace SocialMediaClean.Domain.Entities;
public class Message : BaseEntity
{
    public string MessageSenderUserId { get; set; } = null!;

    public int ConversationsId { get; set; }

    public bool IsSeen { get; set;} = false;

    public string MessageText { get; set; } = null!;

    public virtual User MessageSenderUserNavigation { get; set; } = null!;
    public virtual Conversations ConversationsNavigation { get; set; } = null!;
}
