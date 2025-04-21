using Application.CQRS.Auth_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.User_.Queries.GetUserInfo;

public class GetUserInfoQuery : IRequest<UserResponseDto>
{
    public Guid Id { get; set; }
}

public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
{
    public GetUserInfoQueryValidator()
    {
        RuleFor(b => b.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}
