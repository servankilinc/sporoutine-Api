using FluentValidation;
using MediatR;

namespace Application.CQRS.Auth_.Commands.UpdatePassword;

public class UpdatePasswordCommand : IRequest
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}


public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(b => b.UserId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(r => r.Token).NotNull().NotEmpty();
        RuleFor(b => b.NewPassword).NotNull().NotEmpty().MinimumLength(6);
    }
}
