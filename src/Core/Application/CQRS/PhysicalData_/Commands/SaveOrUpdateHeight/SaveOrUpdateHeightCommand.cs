using FluentValidation;
using MediatR;

namespace Application.CQRS.PhysicalData_.Commands.SaveOrUpdateHeight;

public class SaveOrUpdateHeightCommand : IRequest
{
    public Guid UserId { get; set; }
    public float Height { get; set; }
}

public class SaveOrUpdateHeightCommandValidator : AbstractValidator<SaveOrUpdateHeightCommand>
{
    public SaveOrUpdateHeightCommandValidator()
    {
        RuleFor(r => r.UserId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(r => r.Height).NotNull().NotEmpty().LessThan(400).GreaterThan(10);
    }
}