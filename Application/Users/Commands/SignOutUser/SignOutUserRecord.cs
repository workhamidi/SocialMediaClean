using MediatR;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.Users.Commands.SignOutUser;


public record SignOutUserRecord() : IRequest<Result>;

