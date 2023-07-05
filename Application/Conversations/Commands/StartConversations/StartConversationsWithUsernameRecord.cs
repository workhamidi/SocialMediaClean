using MediatR;
using SocialMediaClean.Application.Common.Attributes.Security;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Common.Models.Identity;

namespace SocialMediaClean.Application.Conversations.Commands.StartConversations;


[Authorize(Roles = $"{nameof(Roles.User)}")]
[Authorize(Policy = $"{nameof(Policies.StartConversation)}")]
public record StartConversationsWithUsernameRecord(
    string ParticipantUsername,string Message) : IRequest<Result>;


