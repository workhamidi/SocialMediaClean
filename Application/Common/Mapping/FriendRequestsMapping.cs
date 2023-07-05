using SocialMediaClean.Application.Common.Models.Dtos;

namespace SocialMediaClean.Application.Common.Mapping;

public static class FriendRequestsMapping
{
    public static NewFriendRequestDto FriendRequestToNewFriendRequestDto(
        this Domain.Entities.FriendRequest friendRequest)
    {
        return new NewFriendRequestDto()
        {
            Username = friendRequest.SenderFriendRequestUserNavigation.UserName
        };
    }
}

