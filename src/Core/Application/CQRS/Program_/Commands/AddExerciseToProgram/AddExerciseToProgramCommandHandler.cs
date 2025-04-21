using Application.CQRS.Program_.Dtos;
using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Program_.Commands.AddExerciseToProgram;

public class AddExerciseToProgramCommandHandler : IRequestHandler<AddExerciseToProgramCommand, ProgramExerciseResponseDto>
{
    private readonly IProgramRepository _programRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;
    public AddExerciseToProgramCommandHandler(IProgramRepository programRepository, IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }


    public async Task<ProgramExerciseResponseDto> Handle(AddExerciseToProgramCommand request, CancellationToken cancellationToken)
    {
        Exercise exercise = await _exerciseRepository.GetAsync(filter: e => e.Id == request.ExerciseId);
        if (exercise is null) throw new BusinessException($"Exercise does not found! ({request.ExerciseId})");
        if (exercise.ExerciseType != request.ExerciseType) throw new BusinessException($"Exercise type does not match with request! ({exercise.ExerciseType}, {request.ExerciseType})");
        if (!ExerciseType.IsDefined(request.ExerciseType)) throw new BusinessException($"Exercise type is not defined! ({request.ExerciseType})");
 
        var dataToInsert = _mapper.Map<ProgramExercise>(request);
        dataToInsert.AddedDate = DateTime.UtcNow;

        ProgramExercise addedData = await _programRepository.AddExerciseToProgramAsync(dataToInsert, cancellationToken);

        return _mapper.Map<ProgramExerciseResponseDto>(addedData);
    }
}
