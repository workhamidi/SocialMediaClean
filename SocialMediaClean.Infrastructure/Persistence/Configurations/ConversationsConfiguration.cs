using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaClean.Domain.Entities;

namespace SocialMediaClean.Infrastructure.Persistence.Configurations;

public class ConversationsConfiguration : IEntityTypeConfiguration<Conversations>
{
    public void Configure(EntityTypeBuilder<Conversations> builder)
    {
        builder
            .HasOne(c=>c.CreatorUserNavigation)
            .WithMany(user => user.ConversationsAsCreatorUser)
            .HasForeignKey(c=>c.CreatorUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(c=>c.FirstParticipantsUserNavigation)
            .WithMany(user => user.ConversationsAsFirstParticipant)
            .HasForeignKey(c=>c.FirstParticipantsUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(c=>c.SecondParticipantsUserNavigation)
            .WithMany(user => user.ConversationsAsSecondParticipant)
            .HasForeignKey(c=>c.SecondParticipantsUserId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}

