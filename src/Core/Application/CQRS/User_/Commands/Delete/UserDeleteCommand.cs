using FluentValidation;
using MediatR;

namespace Application.CQRS.User_.Commands.Delete;

public class UserDeleteCommand : IRequest
{
    public Guid Id { get; set; }
}


public class UserDeleteCommandValidator : AbstractValidator<UserDeleteCommand>
{
    public UserDeleteCommandValidator()
    {
        RuleFor(b => b.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}
