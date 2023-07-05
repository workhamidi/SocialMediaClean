
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaClean.Domain.Entities;

namespace SocialMediaClean.Infrastructure.Persistence.Configurations;

public class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequest>
{
    public void Configure(EntityTypeBuilder<FriendRequest> builder)
    {
        
        builder
            .HasOne(fr => fr.ReceiverFriendRequestUserNavigation)
            .WithMany(user => user.ReceiverFriendRequestNavigation)
            .HasForeignKey(fr => fr.ReceiverFriendRequestUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(fr => fr.SenderFriendRequestUserNavigation)
            .WithMany(user => user.SenderFriendRequestNavigation)
            .HasForeignKey(fr => fr.SenderFriendRequestUserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}


