using Application.CQRS.Exercise_.Dtos;
using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Exercise_.Commands.Update;

public class ExerciseUpdateCommandHandler : IRequestHandler<ExerciseUpdateCommand, ExerciseResponseDto>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;
    public ExerciseUpdateCommandHandler(IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<ExerciseResponseDto> Handle(ExerciseUpdateCommand request, CancellationToken cancellationToken)
    {
        var existExercise = await _exerciseRepository.GetAsync(filter: f => f.Id == request.Id);
        if (existExercise == null) throw new BusinessException("The exercise could not be found to update");
        Exercise exerciseToInsert = _mapper.Map(request, existExercise);
        Exercise updatedExercise = await _exerciseRepository.UpdateAsync(exerciseToInsert, cancellationToken);
        return _mapper.Map<ExerciseResponseDto>(updatedExercise);
    }
}
