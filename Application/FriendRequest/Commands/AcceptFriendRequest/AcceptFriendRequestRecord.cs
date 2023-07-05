using MediatR;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.FriendRequest.Commands.AcceptFriendRequest;

public record AcceptFriendRequestRecord(string UsernameFriendRequestSender) : IRequest<Result>;


