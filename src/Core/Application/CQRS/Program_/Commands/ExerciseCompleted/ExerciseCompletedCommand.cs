using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Commands.ExerciseCompleted;

public class ExerciseCompletedCommand : IRequest
{
    public Guid ProgramExerciseId { get; set; }
}

public class ExerciseCompletedCommandValidator : AbstractValidator<ExerciseCompletedCommand>
{
    public ExerciseCompletedCommandValidator()
    {
        RuleFor(r => r.ProgramExerciseId).NotEmpty().NotNull().NotEqual(f => Guid.Empty);
    }
}
