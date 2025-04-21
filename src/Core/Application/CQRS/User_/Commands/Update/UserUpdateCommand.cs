using Application.CQRS.Auth_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.User_.Commands.Update;

public class UserUpdateCommand : IRequest<UserResponseDto>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}


public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand>
{
    public UserUpdateCommandValidator()
    {
        RuleFor(b => b.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(b => b.FirstName).MinimumLength(2).NotEmpty();
        RuleFor(b => b.LastName).MinimumLength(2).NotEmpty().NotEqual(s => s.FirstName);
    }
}
