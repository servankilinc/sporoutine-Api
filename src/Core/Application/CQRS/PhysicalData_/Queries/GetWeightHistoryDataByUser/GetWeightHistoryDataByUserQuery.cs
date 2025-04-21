using Application.CQRS.PhysicalData_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.PhysicalData_.Queries.GetWeightHistoryDataByUser;

public class GetWeightHistoryDataByUserQuery : IRequest<List<WeightHistoryDataResponseDto>>
{
    public Guid UserId { get; set; }
}

public class GetWeightHistoryDataByUserQueryValidator : AbstractValidator<GetWeightHistoryDataByUserQuery>
{
    public GetWeightHistoryDataByUserQueryValidator()
    {
        RuleFor(r => r.UserId).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}
