using Application.Services.Repositories;
using MediatR;

namespace Application.CQRS.Program_.Commands.ExerciseCompleted;

public class ExerciseCompletedCommandHandler : IRequestHandler<ExerciseCompletedCommand>
{
    private readonly IProgramRepository _programRepository;
    public ExerciseCompletedCommandHandler(IProgramRepository programRepository) => _programRepository = programRepository;

    public async Task Handle(ExerciseCompletedCommand request, CancellationToken cancellationToken)
    {
        await _programRepository.ProgramExerciseCompletedAsync(request.ProgramExerciseId, cancellationToken);
    }
}