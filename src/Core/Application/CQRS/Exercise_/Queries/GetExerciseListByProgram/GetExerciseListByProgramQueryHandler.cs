using Application.CQRS.Exercise_.Models;
using Application.CQRS.Region_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Exercise_.Queries.GetExerciseListByProgram;

public class GetExerciseListByProgramQueryHandler : IRequestHandler<GetExerciseListByProgramQuery, List<ProgramExerciseResponseModel>>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;
    public GetExerciseListByProgramQueryHandler(IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<List<ProgramExerciseResponseModel>> Handle(GetExerciseListByProgramQuery request, CancellationToken cancellationToken)
    {

        List<ProgramExercise> result = await _exerciseRepository.GetExerciseListByProgramAsync(request.ProgramId);

        List<ProgramExerciseResponseModel> responseList = result.Select(pe => new ProgramExerciseResponseModel()
        {
            ExerciseId = pe.Exercise!.Id,
            Name = pe.Exercise.Name,
            Content = pe.Exercise.Content,
            Description = pe.Exercise.Description,
            ExerciseType = pe.Exercise.ExerciseType,
            ProgramExerciseId = pe.Id,
            AddedDate = pe.AddedDate,
            Day = pe.Day,
            NumberOfSets = pe.NumberOfSets,
            NumberOfRepetition = pe.NumberOfRepetition,
            Time = pe.Time,
            Regions = pe.Exercise.RegionExercises != null ? pe.Exercise.RegionExercises.Select(re => _mapper.Map<RegionResponseDto>(re.Region)).ToList() : default(List<RegionResponseDto>)
        }).ToList();


        return responseList;
    }
}