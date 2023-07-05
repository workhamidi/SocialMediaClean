using System.Diagnostics;
using SocialMediaClean.Application;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Infrastructure;
using SocialMediaClean.Infrastructure.Identity;
using SocialMediaClean.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.PresentationDependencyInjection();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


var registerRolePermissionToDatabase =
    app.Services.GetService<IRegisterRolePermissionToDatabase>();

if (registerRolePermissionToDatabase == null)
    app.Lifetime.StopApplication();


registerRolePermissionToDatabase!.Register()
    .GetAwaiter().GetResult();



app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllers();

app.Run();


