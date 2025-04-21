using FluentValidation;
using MediatR;

namespace Application.CQRS.PhysicalData_.Commands.SaveOrUpdateWeight;

public class SaveOrUpdateWeightCommand : IRequest
{
    public Guid UserId { get; set; }
    public float Weight { get; set; }
}

public class SaveOrUpdateWeightCommandValidator : AbstractValidator<SaveOrUpdateWeightCommand>
{
    public SaveOrUpdateWeightCommandValidator()
    {
        RuleFor(r => r.UserId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(r => r.Weight).NotNull().NotEmpty().LessThan(400).GreaterThan(10);
    }
}
