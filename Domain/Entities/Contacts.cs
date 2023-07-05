
namespace SocialMediaClean.Domain.Entities;
public class Contacts : BaseEntity
{
    public string OwnerContactsUserId { get; set; } = null!;
    public string ContactUserId { get; set; } = null!;
    
    public virtual User ContactUserNavigation { get; set; } = null!;
    public virtual User OwnerContactsUserNavigation { get; set; } = null!;
}
