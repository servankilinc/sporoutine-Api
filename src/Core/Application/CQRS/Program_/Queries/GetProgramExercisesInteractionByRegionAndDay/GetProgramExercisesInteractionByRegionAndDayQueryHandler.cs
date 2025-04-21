using Application.CQRS.Exercise_.Dtos;
using Application.CQRS.Program_.Dtos;
using Application.CQRS.Program_.Models;
using Application.CQRS.Region_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetProgramExercisesInteractionByRegionAndDay;

public class GetProgramExercisesInteractionByRegionAndDayQueryHandler : IRequestHandler<GetProgramExercisesInteractionByRegionAndDayQuery, List<ProgramExerciseInteractionModel>>
{
    private readonly IProgramRepository _programRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;
    public GetProgramExercisesInteractionByRegionAndDayQueryHandler(IProgramRepository programRepository, IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<List<ProgramExerciseInteractionModel>> Handle(GetProgramExercisesInteractionByRegionAndDayQuery request, CancellationToken cancellationToken)
    {
        // herhangi bir exercise programda ise programRxercise den gelecek bilgileri sağlayacak
        var programExercises = await _programRepository.GetProgramExercisesByDayAsync(request.ProgramId, request.Day, cancellationToken);
        // istenen region'lara göre bütün egzersizler programda olsun olmasın sağlanacak
        var exercisesByRegion = await _exerciseRepository.GetExerciseListByRegionAsync(request.RegionIds, cancellationToken);


        var responseModel = exercisesByRegion.Select(e =>
            {
                bool isExist = programExercises.Any(pe => pe.ExerciseId == e.Id);
                var item = new ProgramExerciseInteractionModel()
                {
                    IsAdded = isExist,
                    Regions = e.RegionExercises != null ? e.RegionExercises.Select(re => _mapper.Map<RegionResponseDto>(re.Region)).ToList() : default,
                    Exercise = _mapper.Map<ExerciseResponseDto>(e),
                    ProgramExercise = isExist ? _mapper.Map<ProgramExerciseResponseDto>(programExercises.FirstOrDefault(f => f.ExerciseId == e.Id)) : default,
                };
                return item;
            }
        ).ToList();

        return responseModel;
    }
}