namespace SocialMediaClean.Domain.Common;

public class BaseEntity
{
    public int Id { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    public string? CreatedBy { get; set; } 

    public DateTime? LastModified { get; set; } = DateTime.Now;

    public string? LastModifiedBy { get; set; }
}

