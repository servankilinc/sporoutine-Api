using Application.Services.Repositories;
using MediatR;

namespace Application.CQRS.Exercise_.Commands.RemoveRegionFromExercise;

public class RemoveRegionFromExerciseCommandHandler : IRequestHandler<RemoveRegionFromExerciseCommand>
{
    private readonly IExerciseRepository _exerciseRepository;
    public RemoveRegionFromExerciseCommandHandler(IExerciseRepository exerciseRepository) => _exerciseRepository = exerciseRepository;


    public async Task Handle(RemoveRegionFromExerciseCommand request, CancellationToken cancellationToken)
    {
        await _exerciseRepository.RemoveRegionFromExerciseAsync(request, cancellationToken);
    }
}