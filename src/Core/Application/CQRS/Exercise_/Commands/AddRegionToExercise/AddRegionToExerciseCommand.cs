using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Exercise_.Commands.AddRegionToExercise;

public class AddRegionToExerciseCommand : IRequest
{
    public Guid ExerciseId { get; set; }
    public Guid RegionId { get; set; }
}


public class AddRegionToExerciseCommandValidator : AbstractValidator<AddRegionToExerciseCommand>
{
    public AddRegionToExerciseCommandValidator()
    {
        RuleFor(r => r.ExerciseId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(r => r.RegionId).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}
