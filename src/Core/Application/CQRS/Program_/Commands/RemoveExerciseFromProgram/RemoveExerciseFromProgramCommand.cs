using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Commands.RemoveExerciseFromProgram;

public class RemoveExerciseFromProgramCommand : IRequest
{
    public Guid ProgramExerciseId { get; set; }
}

public class RemoveExerciseFromProgramCommandValidator : AbstractValidator<RemoveExerciseFromProgramCommand>
{
    public RemoveExerciseFromProgramCommandValidator()
    {
        RuleFor(r => r.ProgramExerciseId).NotEmpty().NotNull().NotEqual(f => Guid.Empty);
    }
}
