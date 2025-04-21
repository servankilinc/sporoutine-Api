using Application.CQRS.Region_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Region_.Queries.Get;

public class GetRegionQuery : IRequest<RegionResponseDto>
{
    public Guid Id { get; set; }
}

public class GetRegionQueryValidator : AbstractValidator<GetRegionQuery>
{
    public GetRegionQueryValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}
