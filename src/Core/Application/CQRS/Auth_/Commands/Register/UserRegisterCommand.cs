using FluentValidation;
using MediatR;

namespace Application.CQRS.Auth_.Commands.Register;

public class UserRegisterCommand : IRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}


public class UserRegisterValidator : AbstractValidator<UserRegisterCommand>
{
    public UserRegisterValidator()
    {
        RuleFor(b => b.Email).EmailAddress().NotEmpty().EmailAddress();
        RuleFor(b => b.Password).MinimumLength(6).NotEmpty();
        RuleFor(b => b.FirstName).MinimumLength(2).NotEmpty();
        RuleFor(b => b.LastName).MinimumLength(2).NotEmpty().NotEqual(s => s.FirstName);
    }
}