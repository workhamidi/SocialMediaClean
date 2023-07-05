
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaClean.Domain.Entities;

namespace SocialMediaClean.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder
            .HasMany(u => u.ReceiverFriendRequestNavigation)
            .WithOne(f => f.ReceiverFriendRequestUserNavigation)
            .HasForeignKey(f => f.ReceiverFriendRequestUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.SenderFriendRequestNavigation)
            .WithOne(f => f.SenderFriendRequestUserNavigation)
            .HasForeignKey(f => f.SenderFriendRequestUserId)
            .OnDelete(DeleteBehavior.Cascade);




        builder
            .HasMany(u => u.ConversationsAsCreatorUser)
            .WithOne(c => c.CreatorUserNavigation)
            .HasForeignKey(u=>u.CreatorUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.ConversationsAsFirstParticipant)
            .WithOne(c => c.FirstParticipantsUserNavigation)
            .HasForeignKey(u => u.FirstParticipantsUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.ConversationsAsSecondParticipant)
            .WithOne(c => c.SecondParticipantsUserNavigation)
            .HasForeignKey(u => u.SecondParticipantsUserId)
            .OnDelete(DeleteBehavior.Cascade);



        builder
            .HasMany(u => u.ContactsNavigation)
            .WithOne(c => c.ContactUserNavigation)
            .HasForeignKey(c => c.ContactUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.OwnerUserContactsNavigation)
            .WithOne(c => c.OwnerContactsUserNavigation)
            .HasForeignKey(c => c.OwnerContactsUserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}

