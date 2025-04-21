using Application.CQRS.Program_.Dtos;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Commands.UpdateExerciseOfProgram;

public class UpdateExerciseOfProgramCommand : IRequest<ProgramExerciseResponseDto>
{
    public Guid ProgramExerciseId { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public int NumberOfSets { get; set; }
    public int NumberOfRepetition { get; set; }
    public int Time { get; set; }
}


public class UpdateExerciseOfProgramCommandValidator : AbstractValidator<UpdateExerciseOfProgramCommand>
{
    public UpdateExerciseOfProgramCommandValidator()
    {
        RuleFor(r => r.ProgramExerciseId).NotEmpty().NotNull().NotEqual(f => Guid.Empty);

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