namespace SocialMediaClean.Domain.Entities;

public class FriendRequest : BaseEntity
{
   public string ReceiverFriendRequestUserId { get; set; } = null!;

    public string SenderFriendRequestUserId { get; set; } = null!;

    public bool ReceiverIsSeenRequest { get; set; } = false;

    public bool ReceiverIsAcceptedRequest { get; set; } = false;

    public virtual User ReceiverFriendRequestUserNavigation { get; set; } = null!;

    public virtual User SenderFriendRequestUserNavigation { get; set; } = null!;

}

