using Application.CQRS.Program_.Models;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetProgramExercisesInteractionByRegion;

public class GetProgramExercisesInteractionByRegionQuery : IRequest<List<ProgramExerciseInteractionMultipleDayModel>>
{
    public Guid ProgramId { get; set; }
    public Guid[] RegionIds { get; set; } = null!;
}

public class GetProgramExercisesInteractionByRegionQueryValidator : AbstractValidator<GetProgramExercisesInteractionByRegionQuery>
{
    public GetProgramExercisesInteractionByRegionQueryValidator()
    {
        RuleFor(r => r.ProgramId).NotEmpty().NotNull().NotEqual(f => Guid.Empty);
        RuleFor(r => r.RegionIds).NotNull().NotEqual([]);
    }
}
