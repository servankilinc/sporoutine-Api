using FluentValidation;
using MediatR;

namespace Application.CQRS.Auth_.Commands.ConfirmPassworReset;

public class ConfirmPassworResetCommand : IRequest<ConfirmPassworResetCommandResponseModel>
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = null!;
}


public class ConfirmPassworResetCommandValidator : AbstractValidator<ConfirmPassworResetCommand>
{
    public ConfirmPassworResetCommandValidator()
    {
        RuleFor(b => b.UserId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(b => b.Token).NotNull().NotEmpty();
    }
}
