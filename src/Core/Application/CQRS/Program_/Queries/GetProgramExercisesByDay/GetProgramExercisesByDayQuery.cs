using Application.CQRS.Program_.Models;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetProgramExercisesByDay;

public class GetProgramExercisesByDayQuery : IRequest<List<ProgramExerciseBaseModel>>
{
    public Guid ProgramId { get; set; }
    public int Day { get; set; }
}

public class GetProgramExercisesByDayValidator : AbstractValidator<GetProgramExercisesByDayQuery>
{
    public GetProgramExercisesByDayValidator()
    {
        RuleFor(r => r.ProgramId).NotEmpty().NotNull().NotEqual(Guid.Empty);
        RuleFor(r => r.Day).GreaterThan(0).LessThan(8).NotEmpty().NotNull();
    }
}
