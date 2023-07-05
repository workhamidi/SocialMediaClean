using MediatR;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.Users.Commands.LoginUser;

public record LogInUserRecord(string Username, string Password) : IRequest<Result>;

