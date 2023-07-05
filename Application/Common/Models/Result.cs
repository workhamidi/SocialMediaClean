using SocialMediaClean.Application.Common.Enums;

namespace SocialMediaClean.Application.Common.Models;
public class Result
{
    public ResultCodeEnum ResultCode { get; set; } = ResultCodeEnum.None;

    public string? JsonData { get; set; }

    public string? Description { get; set; }

    public string? Token { get; set; }

    public Dictionary<string, string>? Errors { get; set; }



}

