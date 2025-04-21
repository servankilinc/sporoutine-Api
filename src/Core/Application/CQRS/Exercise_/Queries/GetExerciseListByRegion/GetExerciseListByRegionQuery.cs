using Application.CQRS.Exercise_.Models;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Exercise_.Queries.GetExerciseListByRegion;

public class GetExerciseListByRegionQuery : IRequest<List<ExerciseResponseModel>>
{
    public Guid[]? RegionIds { get; set; }
}


public class GetExerciseListByRegionQueryValidator : AbstractValidator<GetExerciseListByRegionQuery>
{
    public GetExerciseListByRegionQueryValidator()
    {
        RuleFor(r => r.RegionIds).NotNull().NotEqual([]);
    }
}
