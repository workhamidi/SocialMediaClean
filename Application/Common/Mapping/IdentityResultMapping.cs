using Microsoft.AspNetCore.Identity;
using SocialMediaClean.Application.Common.Enums;
using SocialMediaClean.Application.Common.Models;

namespace SocialMediaClean.Application.Common.Mapping;

public static class IdentityResultMapping
{
    public static Result IdentityResultToResult(
        this IdentityResult identityResult,
        string? token = null,
        string? description = null,
        string? jsonData = null)
    {
        return new Result()
        {
            Errors = identityResult.Errors?.ToDictionary(
                errorCode => errorCode.Code ?? (-1).ToString(),
                errorDescription => errorDescription.Description
                ),
            ResultCode = identityResult.Succeeded ? ResultCodeEnum.Success : ResultCodeEnum.Error,
            Description = description,
            Token = token,
            JsonData = jsonData
        };
    }


}

