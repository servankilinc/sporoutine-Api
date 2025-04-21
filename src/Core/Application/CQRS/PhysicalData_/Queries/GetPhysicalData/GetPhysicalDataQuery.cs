using Application.CQRS.PhysicalData_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.PhysicalData_.Queries.GetPhysicalData;

public class GetPhysicalDataQuery : IRequest<PhysicalDataResponseDto>
{
    public Guid UserId { get; set; }
}

public class GetPhysicalDataQueryValidator : AbstractValidator<GetPhysicalDataQuery>
{
    public GetPhysicalDataQueryValidator()
    {
        RuleFor(r => r.UserId).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}
