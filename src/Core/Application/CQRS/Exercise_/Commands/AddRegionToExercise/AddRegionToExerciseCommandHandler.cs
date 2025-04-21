using Application.Services.Repositories;
using MediatR;

namespace Application.CQRS.Exercise_.Commands.AddRegionToExercise;

public class AddRegionToExerciseCommandHandler : IRequestHandler<AddRegionToExerciseCommand>
{
    private readonly IExerciseRepository _exerciseRepository; 
    public AddRegionToExerciseCommandHandler(IExerciseRepository exerciseRepository) => _exerciseRepository = exerciseRepository; 
    

    public async Task Handle(AddRegionToExerciseCommand request, CancellationToken cancellationToken)
    {
        await _exerciseRepository.AddRegionToExerciseAsync(request, cancellationToken);
    }
}
