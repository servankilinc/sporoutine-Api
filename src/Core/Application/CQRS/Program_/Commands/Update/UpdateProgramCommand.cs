using Application.CQRS.Program_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Commands.Update;

public class UpdateProgramCommand : IRequest<ProgramResponseDto>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class UpdateProgramCommandValidator : AbstractValidator<UpdateProgramCommand>
{
    public UpdateProgramCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty().NotNull().NotEqual(f => Guid.Empty);
        RuleFor(r => r.Name).NotEmpty().NotNull();
    }
}
