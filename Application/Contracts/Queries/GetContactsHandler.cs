using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMediaClean.Application.Common.Enums;
using SocialMediaClean.Application.Common.Interfaces;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.Contracts.Queries;

public class GetContactsHandler : IRequestHandler<GetContactsRecord, Result>
{
    private readonly ISocialMediaCleanContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetContactsHandler(
        ISocialMediaCleanContext context,
        ICurrentUserService currentUserService
    )
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(GetContactsRecord request, CancellationToken cancellationToken)
    {
        var contacts = _context.Contacts
            .Include(c => c.ContactUserNavigation)
            .Where(c => c.OwnerContactsUserId == _currentUserService.UserId)
            .ToList();

        
        if (contacts is null || contacts.Count == 0)
            return new Result()
            {
                ResultCode = ResultCodeEnum.Failed,
                Description = "the user don't have any contacts"
            };


        var contactsJson = JsonSerializer.Serialize(
            contacts.Select(c => new
            {
                Username = c.ContactUserNavigation.UserName
            })
        );


        return new Result()
        {
            JsonData = contactsJson
        };

    }

}

