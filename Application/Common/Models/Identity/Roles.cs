using Microsoft.AspNetCore.Identity;

namespace SocialMediaClean.Application.Common.Models.Identity;

public class Roles
{
    
    
    public static IdentityRole User = new IdentityRole()
    {
        Name = "User"
    };
    

    public static IEnumerable<IdentityRole> List()
    {
        yield return User;
    }
}
