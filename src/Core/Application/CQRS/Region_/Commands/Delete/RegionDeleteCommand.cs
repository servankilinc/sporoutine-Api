using FluentValidation;
using MediatR;

namespace Application.CQRS.Region_.Commands.Delete;

public class RegionDeleteCommand : IRequest
{
    public Guid Id { get; set; }
}

public class RegionDeleteCommandValidator : AbstractValidator<RegionDeleteCommand>
{
    public RegionDeleteCommandValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}
