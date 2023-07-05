using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Domain.Entities;

namespace SocialMediaClean.Infrastructure.Persistence.Context;


public class SocialMediaCleanContext : ApiAuthorizationDbContext<User>, ISocialMediaCleanContext
{
    public SocialMediaCleanContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }


    #region Methodes

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public int SaveChangesAsync()
    {
       return base.SaveChanges();
    }


    #endregion



    #region Models

    public virtual DbSet<User> User { get; set; } = null!;
    public virtual DbSet<Message> Message { get; set; } = null!;
    public virtual DbSet<Contacts> Contacts { get; set; } = null!;
    public virtual DbSet<Conversations> Conversations { get; set; } = null!;
    public virtual DbSet<FriendRequest> FriendRequest { get; set; } = null!;

    #endregion


}
