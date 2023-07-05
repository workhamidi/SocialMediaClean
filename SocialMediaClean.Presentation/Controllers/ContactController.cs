using Microsoft.AspNetCore.Mvc;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Contracts.Queries;

namespace SocialMediaClean.Presentation.Controllers;

public class ContactController : ApiControllerBase
{
    [HttpGet("GetContacts")]
    public async Task<Result> GetContactsAsync()
    {
        return await Mediator.Send(new GetContactsRecord());
    }
}

