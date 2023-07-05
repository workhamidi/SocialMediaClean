using MediatR;
using SocialMediaClean.Application.Common.Attributes.Security;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Common.Models.Identity;

namespace SocialMediaClean.Application.Users.Queries.GetAllUsers;


[Authorize(Roles = $"{nameof(Roles.User)}")]
[Authorize(Policy = $"{nameof(Policies.SearchUser)}")]
public record AllUsersRecord() : IRequest<Result>;

