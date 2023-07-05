using MediatR;
using SocialMediaClean.Application.Common.Attributes.Security;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Common.Models.Identity;

namespace SocialMediaClean.Application.Message.Commands.EditMessage;

[Authorize(Roles = $"{nameof(Roles.User)}")]
[Authorize(Policy = $"{nameof(Policies.EditMessage)}")]
public record EditMessageRecord(int MessageId,string NewMessageText) : IRequest<Result>;