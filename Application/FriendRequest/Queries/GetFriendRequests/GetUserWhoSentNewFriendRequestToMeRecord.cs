using MediatR;
using SocialMediaClean.Application.Common.Attributes.Security;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Common.Models.Identity;

namespace SocialMediaClean.Application.FriendRequest.Queries.GetFriendRequests;


[Authorize(Roles = $"{nameof(Roles.User)}")]
[Authorize(Policy = $"{nameof(Policies.GetFriendRequests)}")]
public record GetUserWhoSentNewFriendRequestToMeRecord() : IRequest<Result>;



