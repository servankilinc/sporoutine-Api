using Application.CQRS.Program_.Models;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetProgramExercisesInteractionByRegionAndDay;

public class GetProgramExercisesInteractionByRegionAndDayQuery : IRequest<List<ProgramExerciseInteractionModel>>
{
    public Guid ProgramId { get; set; }
    public int Day { get; set; }
    public Guid[] RegionIds { get; set; } = null!;
}

public class GetProgramExercisesInteractionByRegionAndDayQueryValidator : AbstractValidator<GetProgramExercisesInteractionByRegionAndDayQuery>
{
    public GetProgramExercisesInteractionByRegionAndDayQueryValidator()
    {
        RuleFor(r => r.ProgramId).NotEmpty().NotNull().NotEqual(f => Guid.Empty);
        RuleFor(r => r.Day).GreaterThan(0).LessThan(8).NotEmpty().NotNull();
        RuleFor(r => r.RegionIds).NotNull().NotEqual([]);
    }
}
