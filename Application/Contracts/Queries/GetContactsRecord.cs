using MediatR;
using SocialMediaClean.Application.Common.Attributes.Security;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Common.Models.Identity;

namespace SocialMediaClean.Application.Contracts.Queries;

[Authorize(Roles = $"{nameof(Roles.User)}")]
[Authorize(Policy = $"{nameof(Policies.GetContacts)}")]
public record GetContactsRecord() : IRequest<Result>;

