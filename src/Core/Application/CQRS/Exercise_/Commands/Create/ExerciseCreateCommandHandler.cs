using Application.CQRS.Exercise_.Dtos;
using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Exercise_.Commands.Create;

public class ExerciseCreateCommandHandler : IRequestHandler<ExerciseCreateCommand, ExerciseResponseDto>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;
    public ExerciseCreateCommandHandler(IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<ExerciseResponseDto> Handle(ExerciseCreateCommand request, CancellationToken cancellationToken)
    {
        if (!ExerciseType.IsDefined(request.ExerciseType)) throw new BusinessException("Exercise type is not defined! (" + request.ExerciseType + ")" );
        Exercise exerciseToInsert = _mapper.Map<Exercise>(request);
        Exercise insertedExercise = await _exerciseRepository.AddAsync(exerciseToInsert, cancellationToken);
        return _mapper.Map<ExerciseResponseDto>(insertedExercise);
    }
}
