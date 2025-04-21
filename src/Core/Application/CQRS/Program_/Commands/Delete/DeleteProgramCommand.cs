using Application.CQRS.Program_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Commands.Delete;

public class DeleteProgramCommand : IRequest
{
    public Guid ProgramId { get; set; }
}

public class DeleteProgramCommandValidator : AbstractValidator<DeleteProgramCommand>
{
    public DeleteProgramCommandValidator()
    {
        RuleFor(r => r.ProgramId).NotEmpty().NotNull().NotEqual(f => Guid.Empty);
    }
}
