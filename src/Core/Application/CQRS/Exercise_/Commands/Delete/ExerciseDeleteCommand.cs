using FluentValidation;
using MediatR;

namespace Application.CQRS.Exercise_.Commands.Delete;

public class ExerciseDeleteCommand : IRequest
{
    public Guid ExerciseId { get; set; }
}

public class ExerciseDeleteCommandValidator : AbstractValidator<ExerciseDeleteCommand>
{
    public ExerciseDeleteCommandValidator()
    {
        RuleFor(r => r.ExerciseId).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}