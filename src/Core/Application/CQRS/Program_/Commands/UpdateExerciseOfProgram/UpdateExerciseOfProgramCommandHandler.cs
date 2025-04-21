using Application.CQRS.Program_.Dtos;
using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Program_.Commands.UpdateExerciseOfProgram;

public class UpdateExerciseOfProgramCommandHandler : IRequestHandler<UpdateExerciseOfProgramCommand, ProgramExerciseResponseDto>
{
    private readonly IProgramRepository _programRepository;
    private readonly IMapper _mapper;
    public UpdateExerciseOfProgramCommandHandler(IProgramRepository programRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _mapper = mapper;
    }
 

    public async Task<ProgramExerciseResponseDto> Handle(UpdateExerciseOfProgramCommand request, CancellationToken cancellationToken)
    {
        var existData = await _programRepository.GetProgramExerciseAsync(request.ProgramExerciseId);
        if (existData.Exercise!.ExerciseType != request.ExerciseType) throw new BusinessException($"Exercise type does not match with request! ({existData.Exercise.ExerciseType}, {request.ExerciseType})");
        
        var dataToUpdate = _mapper.Map(request, existData);
        ProgramExercise updatedData = await _programRepository.UpdateExerciseOfProgramAsync(dataToUpdate, cancellationToken);

        return _mapper.Map<ProgramExerciseResponseDto>(updatedData);
    }
}
