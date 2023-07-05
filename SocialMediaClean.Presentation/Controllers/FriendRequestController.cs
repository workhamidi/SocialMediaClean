using Microsoft.AspNetCore.Mvc;
using SocialMediaClean.Application.Common.Models;
using SocialMediaClean.Application.FriendRequest.Commands.AcceptFriendRequest;
using SocialMediaClean.Application.FriendRequest.Commands.SendFriendRequest;
using SocialMediaClean.Application.FriendRequest.Queries.GetFriendRequests;

namespace SocialMediaClean.Presentation.Controllers;

public class FriendRequestController : ApiControllerBase
{
    [HttpPost("SendFriendRequest")]
    public async Task<Result> SendFriendRequest([FromBody] SendFriendRequestRecord friendRequestRecord)
    {
        return await Mediator.Send(friendRequestRecord);
    }


    [HttpGet("GetUserWhoSentNewFriendRequestToMe")]
    public async Task<Result> GetUserWhoSentNewFriendRequestToMe()
    {
        return await Mediator.Send(new GetUserWhoSentNewFriendRequestToMeRecord());
    }


    [HttpPost("AcceptFriendRequest")]
    public async Task<Result> AcceptFriendRequest([FromBody] AcceptFriendRequestRecord acceptFriendRequestRecord)
    {
        return await Mediator.Send(acceptFriendRequestRecord);
    }

}

