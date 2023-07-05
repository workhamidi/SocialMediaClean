using Microsoft.AspNetCore.Identity;

namespace SocialMediaClean.Domain.Entities;
public class User : IdentityUser
{
    public DateTime? Created { get; set; } = DateTime.Now;

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; } = DateTime.Now;

    public string? LastModifiedBy { get; set; }
    

    public virtual ICollection<Contacts>? OwnerUserContactsNavigation { get; set; }
    public virtual ICollection<Contacts>? ContactsNavigation { get; set; }



    public virtual ICollection<Conversations>? ConversationsAsFirstParticipant { get; set; }
    public virtual ICollection<Conversations>? ConversationsAsSecondParticipant { get; set; }
    public virtual ICollection<Conversations>? ConversationsAsCreatorUser { get; set; }

    
    public virtual ICollection<Message>? MessageNavigation { get; set; }


    public virtual ICollection<FriendRequest>? SenderFriendRequestNavigation { get; set; }
    public virtual ICollection<FriendRequest>? ReceiverFriendRequestNavigation { get; set; }


}


