using Application.Services.Repositories;
using MediatR;

namespace Application.CQRS.Exercise_.Commands.Delete;

public class ExerciseDeleteCommandHandler : IRequestHandler<ExerciseDeleteCommand>
{
    private readonly IExerciseRepository _exerciseRepository;
    public ExerciseDeleteCommandHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task Handle(ExerciseDeleteCommand request, CancellationToken cancellationToken)
    {
        await _exerciseRepository.DeleteByFilterAsync(filter: f => f.Id == request.ExerciseId);
    }
}
