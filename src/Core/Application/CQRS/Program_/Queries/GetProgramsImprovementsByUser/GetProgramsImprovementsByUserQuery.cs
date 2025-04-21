using Application.CQRS.Program_.Models;
using FluentValidation;
using MediatR;
namespace Application.CQRS.Program_.Queries.GetProgramsImprovementsByUser;

public class GetProgramsImprovementsByUserQuery : IRequest<List<ProgramImprovementModel>>
{
    public Guid UserId { get; set; }
}

public class GetProgramsImprovementsByUserQueryValidator : AbstractValidator<GetProgramsImprovementsByUserQuery>
{
    public GetProgramsImprovementsByUserQueryValidator()
    {
        RuleFor(r => r.UserId).NotEmpty().NotNull().NotEqual(Guid.Empty);
    }
}
