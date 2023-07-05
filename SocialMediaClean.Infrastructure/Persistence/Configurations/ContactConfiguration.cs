using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaClean.Domain.Entities;

namespace SocialMediaClean.Infrastructure.Persistence.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contacts>
{
    public void Configure(EntityTypeBuilder<Contacts> builder)
    {
        builder
            .HasOne(c => c.ContactUserNavigation)
            .WithMany(user => user.ContactsNavigation)
            .HasForeignKey(c=>c.ContactUserId)
            .OnDelete(DeleteBehavior.NoAction);


        builder
            .HasOne(c => c.OwnerContactsUserNavigation)
            .WithMany(user => user.OwnerUserContactsNavigation)
            .HasForeignKey(c => c.OwnerContactsUserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

