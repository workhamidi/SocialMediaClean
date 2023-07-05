using MediatR;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.Users.Commands.CreateUser;

public record CreateUserCommandRecord(string UserName,string Password):IRequest<Result>;

