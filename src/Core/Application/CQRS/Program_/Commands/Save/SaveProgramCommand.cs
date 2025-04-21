using Application.CQRS.Program_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Commands.Save;

public class SaveProgramCommand : IRequest<ProgramResponseDto>
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
}

public class SaveProgramCommandValidator : AbstractValidator<SaveProgramCommand>
{
    public SaveProgramCommandValidator()
    {
        RuleFor(r => r.UserId).NotEmpty().NotNull().NotEqual(f => Guid.Empty);
        RuleFor(r => r.Name).NotEmpty().NotNull();
    }
}
