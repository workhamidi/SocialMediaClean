namespace SocialMediaClean.Domain.Entities;

public class Conversations : BaseEntity
{
    public string CreatorUserId { get; set; } = null!;
    public string FirstParticipantsUserId { get; set; } = null!;
    public string SecondParticipantsUserId { get; set; } = null!;

    public virtual User FirstParticipantsUserNavigation { get; set; } = null!;
    public virtual User SecondParticipantsUserNavigation { get; set; } = null!;
    
    public virtual User CreatorUserNavigation { get; set; } = null!;
    
    public virtual ICollection<Message>? MessageNavigation { get; set; }

}

