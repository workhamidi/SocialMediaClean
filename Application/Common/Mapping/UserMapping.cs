using SocialMediaClean.Application.Users.Commands.CreateUser;
using SocialMediaClean.Domain.Entities;

namespace SocialMediaClean.Application.Common.Mapping;
public static class UserMapping
{
    public static User CreateUserCommandRecordToUser(this CreateUserCommandRecord createUserCommandRecord)
    {
        return new User()
        {
            UserName = createUserCommandRecord.UserName
        };
    }

    public static CreateUserCommandRecord UserToCreateUserCommandRecord(this User user)
    {
        return new CreateUserCommandRecord(user.UserName,user.PasswordHash);
    }
}

