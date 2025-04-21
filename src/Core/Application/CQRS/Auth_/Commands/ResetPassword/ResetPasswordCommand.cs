using FluentValidation;
using MediatR;

namespace Application.CQRS.Auth_.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest
{
    public string? Email { get; set; }
}


public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(r => r.Email).NotNull().NotEmpty().EmailAddress();
    }
}
