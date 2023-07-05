using MediatR;
using SocialMediaClean.Application.Common.Attributes.Security;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Common.Models.Identity;

namespace SocialMediaClean.Application.FriendRequest.Commands.SendFriendRequest;



[Authorize(Roles = $"{nameof(Roles.User)}")]
[Authorize(Policy = $"{nameof(Policies.SendFriendRequest)}")]
public record SendFriendRequestRecord(string Username) : IRequest<Result>;


