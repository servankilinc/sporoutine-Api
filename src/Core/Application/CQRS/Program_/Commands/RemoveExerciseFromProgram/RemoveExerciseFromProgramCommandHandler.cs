using Application.Services.Repositories;
using MediatR;

namespace Application.CQRS.Program_.Commands.RemoveExerciseFromProgram;

public class RemoveExerciseFromProgramCommandHandler : IRequestHandler<RemoveExerciseFromProgramCommand>
{
    private readonly IProgramRepository _programRepository;
    public RemoveExerciseFromProgramCommandHandler(IProgramRepository programRepository) => _programRepository = programRepository;

    public async Task Handle(RemoveExerciseFromProgramCommand request, CancellationToken cancellationToken)
    {
        await _programRepository.RemoveExerciseFromProgramAsync(request.ProgramExerciseId, cancellationToken);
    }
}
