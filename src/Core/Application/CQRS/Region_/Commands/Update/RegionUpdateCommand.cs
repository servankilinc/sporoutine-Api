using Application.CQRS.Region_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Region_.Commands.Update;

public class RegionUpdateCommand : IRequest<RegionResponseDto>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Content { get; set; }
}


public class RegionUpdateCommandValidator : AbstractValidator<RegionUpdateCommand>
{
    public RegionUpdateCommandValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        RuleFor(r => r.Name).NotNull().NotEmpty();
        RuleFor(r => r.Content).NotNull().NotEmpty();
    }
}
