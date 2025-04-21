using FluentValidation;
using MediatR;

namespace Application.CQRS.Auth_.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<ConfirmEmailCommandResponseModel>
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;
}


public class UserConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public UserConfirmEmailCommandValidator()
    {
        RuleFor(b => b.UserId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(b => b.Token).NotNull().NotEmpty();
    }
}
