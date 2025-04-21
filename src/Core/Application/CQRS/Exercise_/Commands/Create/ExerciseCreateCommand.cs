using Application.CQRS.Exercise_.Dtos;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Exercise_.Commands.Create;

public class ExerciseCreateCommand : IRequest<ExerciseResponseDto>
{
    public string? Name { get; set; }
    public string? Content { get; set; }
    public string? Description { get; set; }
    public ExerciseType ExerciseType { get; set; }
}

public class ExerciseCreateCommandValidator : AbstractValidator<ExerciseCreateCommand>
{
    public ExerciseCreateCommandValidator()
    {
        RuleFor(r => r.Name).NotNull().NotEmpty();
        RuleFor(r => r.Content).NotNull().NotEmpty();
        RuleFor(r => r.Description).NotNull().NotEmpty();
        RuleFor(r => r.ExerciseType).NotNull().NotEmpty();
    }
}
