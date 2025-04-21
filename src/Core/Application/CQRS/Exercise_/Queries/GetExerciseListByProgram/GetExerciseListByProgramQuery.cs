using Application.CQRS.Exercise_.Models;
using FluentValidation;
using MediatR;

namespace Application.CQRS.Exercise_.Queries.GetExerciseListByProgram;

public class GetExerciseListByProgramQuery : IRequest<List<ProgramExerciseResponseModel>>
{
    public Guid ProgramId { get; set; }
}

public class GetExerciseListByProgramQueryValidator : AbstractValidator<GetExerciseListByProgramQuery>
{
    public GetExerciseListByProgramQueryValidator()
    {
        RuleFor(r => r.ProgramId).NotNull().NotEmpty().NotEqual(Guid.Empty);
    }
}
