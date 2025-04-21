using Application.CQRS.Program_.Dtos;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Commands.AddExerciseToProgram;

public class AddExerciseToProgramCommand : IRequest<ProgramExerciseResponseDto>
{
    public Guid ProgramId { get; set; }
    public Guid ExerciseId { get; set; } 
    public int Day { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public int NumberOfSets { get; set; }
    public int NumberOfRepetition { get; set; }
    public int Time { get; set; }
}

public class AddExerciseToProgramCommandValidator : AbstractValidator<AddExerciseToProgramCommand>
{
    public AddExerciseToProgramCommandValidator()
    {
        RuleFor(r => r.ProgramId).NotEmpty().NotNull().NotEqual(f => Guid.Empty);
        RuleFor(r => r.ExerciseId).NotEmpty().NotNull().NotEqual(f => Guid.Empty);
        RuleFor(r => r.Day).GreaterThan(0).LessThan(8).NotEmpty().NotNull();

        RuleFor(r => r.ExerciseType).NotNull().NotEmpty();
        When(r => r.ExerciseType == ExerciseType.Weight, () =>
        {
            RuleFor(r => r.NumberOfRepetition).GreaterThan(0).WithMessage("Number of Repetitions must be greater than 0!");
            RuleFor(r => r.NumberOfSets).GreaterThan(0).WithMessage("Number of Sets must be greater than 0!");
        });
         
        When(r => r.ExerciseType == ExerciseType.Cardio, () =>
        {
            RuleFor(r => r.Time).GreaterThan(0).WithMessage("TTime must be greater than 0!");
        });
    }
}
