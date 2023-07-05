using Microsoft.AspNetCore.Mvc;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Message.Commands.EditMessage;
using SocialMediaClean.Application.Message.Commands.SendMessage;
using SocialMediaClean.Application.Message.Queries.GetNewMessages;

namespace SocialMediaClean.Presentation.Controllers;

public class MessageController : ApiControllerBase
{
    [HttpGet("GetNewMessages")]
    public async Task<Result> RegisterUserAsync()
    {
        return await Mediator.Send(new GetNewMessagesRecord());
    }

    [HttpPost("SendMessage")]
    public async Task<Result> SendMessage([FromBody]SendMessage message)
    {
        return await Mediator.Send(message);
    }

    [HttpPost("EditMessage")]
    public async Task<Result> EditMessage([FromBody] EditMessageRecord message)
    {
        return await Mediator.Send(message);
    }

}

