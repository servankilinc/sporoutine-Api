using FluentValidation;

namespace Application.CQRS.Auth_.Commands.Login.Models;

public class UserLoginRequestModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}


public class UserLoginRequestModelValidator : AbstractValidator<UserLoginRequestModel>
{
    public UserLoginRequestModelValidator()
    {
        RuleFor(b => b.Email).EmailAddress().NotEmpty().EmailAddress();
        RuleFor(b => b.Password).MinimumLength(6).NotEmpty();
    }
}