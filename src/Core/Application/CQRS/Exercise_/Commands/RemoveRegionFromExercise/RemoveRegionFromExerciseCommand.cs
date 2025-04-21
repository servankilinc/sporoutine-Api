using FluentValidation;
using MediatR;

namespace Application.CQRS.Exercise_.Commands.RemoveRegionFromExercise;

public class RemoveRegionFromExerciseCommand : IRequest
{
    public Guid ExerciseId { get; set; }
    public Guid RegionId { get; set; }
}


public class RemoveRegionFromExerciseCommandValidator : AbstractValidator<RemoveRegionFromExerciseCommand>
{
    public RemoveRegionFromExerciseCommandValidator()
    {
        RuleFor(r => r.ExerciseId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(r => r.RegionId).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}
