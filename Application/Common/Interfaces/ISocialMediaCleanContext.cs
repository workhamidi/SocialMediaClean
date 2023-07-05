using Microsoft.EntityFrameworkCore;
using SocialMediaClean.Domain.Entities;

namespace SocialMediaClean.Application.Common.Interfaces;

public interface ISocialMediaCleanContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Domain.Entities.Message> Message { get; set; }
    public DbSet<Contacts> Contacts { get; set; }
    public DbSet<Domain.Entities.Conversations> Conversations { get; set; }
    public DbSet<Domain.Entities.FriendRequest> FriendRequest { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    public int SaveChangesAsync();
}
