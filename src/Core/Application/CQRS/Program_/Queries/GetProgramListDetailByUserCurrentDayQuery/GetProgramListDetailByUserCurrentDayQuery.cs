using Application.CQRS.Program_.Models;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetProgramListDetailByUserCurrentDayQuery;

public class GetProgramListDetailByUserCurrentDayQuery : IRequest<List<ProgramDetailCurrentDayResponseModel>>
{
    public Guid UserId { get; set; }
}

public class GetProgramListDetailByUserCurrentDayQueryValidator : AbstractValidator<GetProgramListDetailByUserCurrentDayQuery>
{
    public GetProgramListDetailByUserCurrentDayQueryValidator()
    {
        RuleFor(r => r.UserId).NotEmpty().NotNull().NotEqual(Guid.Empty);
    }
}
