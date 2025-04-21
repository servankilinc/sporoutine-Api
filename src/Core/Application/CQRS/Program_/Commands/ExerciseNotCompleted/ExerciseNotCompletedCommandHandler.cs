using Application.Services.Repositories;
using MediatR;

namespace Application.CQRS.Program_.Commands.ExerciseNotCompleted;

public class ExerciseNotCompletedCommandHandler : IRequestHandler<ExerciseNotCompletedCommand>
{
    private readonly IProgramRepository _programRepository;
    public ExerciseNotCompletedCommandHandler(IProgramRepository programRepository) => _programRepository = programRepository;

    public async Task Handle(ExerciseNotCompletedCommand request, CancellationToken cancellationToken)
    {
        await _programRepository.ProgramExerciseNotCompletedAsync(request.ProgramExerciseId, cancellationToken);
    }
}