using Microsoft.AspNetCore.Mvc;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.Conversations.Commands.StartConversations;

namespace SocialMediaClean.Presentation.Controllers;
public class ConversationsController : ApiControllerBase
{

    [HttpPost("StartConversationsWithUsername")]
    public async Task<Result> StartConversationsAsync([FromBody] StartConversationsWithUsernameRecord record)
    {
        return await Mediator.Send(record);
    }
    
}