using Application.CQRS.Region_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Region_.Commands.Create;

public class RegionCreateCommand : IRequest<RegionResponseDto>
{
    public string? Name { get; set; }
    public string? Content { get; set; }
}

public class RegionCreateCommandValidator : AbstractValidator<RegionCreateCommand>
{
    public RegionCreateCommandValidator()
    {
        RuleFor(r => r.Name).NotNull().NotEmpty();
        RuleFor(r => r.Content).NotNull().NotEmpty();
    }
}
