using FluentValidation;

namespace SocialMediaClean.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidation : AbstractValidator<CreateUserCommandRecord>
{
    public CreateUserCommandValidation()
    {
        RuleFor(rec => rec.UserName)
            .NotEmpty().WithMessage("Please specify a UserName");


        RuleFor(rec => rec.Password)
            .NotEmpty().WithMessage("Please specify a Password");
    }
}


