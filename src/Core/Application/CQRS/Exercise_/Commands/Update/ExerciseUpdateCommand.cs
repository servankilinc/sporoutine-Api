using Application.CQRS.Exercise_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Exercise_.Commands.Update;

public class ExerciseUpdateCommand : IRequest<ExerciseResponseDto>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
    public string? Description { get; set; }
}

public class ExerciseUpdateCommandValidator : AbstractValidator<ExerciseUpdateCommand>
{
    public ExerciseUpdateCommandValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(r => r.Name).NotNull().NotEmpty();
        RuleFor(r => r.Content).NotNull().NotEmpty();
        RuleFor(r => r.Description).NotNull().NotEmpty();
    }
}
