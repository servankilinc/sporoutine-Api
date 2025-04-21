using Application.CQRS.Program_.Dtos;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetProgramListByUser;

public class GetProgramListByUserQuery : IRequest<List<ProgramResponseDto>>
{
    public Guid UserId { get; set; }
}

public class GetProgramListByUserQueryValidator : AbstractValidator<GetProgramListByUserQuery>
{
    public GetProgramListByUserQueryValidator()
    {
        RuleFor(r => r.UserId).NotEmpty().NotNull().NotEqual(f => Guid.Empty);
    }
}
