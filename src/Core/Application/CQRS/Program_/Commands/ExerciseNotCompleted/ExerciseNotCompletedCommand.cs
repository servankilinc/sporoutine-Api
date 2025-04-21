using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Commands.ExerciseNotCompleted;

public class ExerciseNotCompletedCommand : IRequest
{
    public Guid ProgramExerciseId { get; set; }
}
public class ExerciseNotCompletedCommandValidator : AbstractValidator<ExerciseNotCompletedCommand>
{
    public ExerciseNotCompletedCommandValidator()
    {
        RuleFor(r => r.ProgramExerciseId).NotEmpty().NotNull().NotEqual(f => Guid.Empty);
    }
}