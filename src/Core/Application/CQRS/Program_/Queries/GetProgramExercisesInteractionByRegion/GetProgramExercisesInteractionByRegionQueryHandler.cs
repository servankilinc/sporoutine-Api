using Application.CQRS.Exercise_.Dtos;
using Application.CQRS.Program_.Dtos;
using Application.CQRS.Program_.Models;
using Application.CQRS.Region_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Program_.Queries.GetProgramExercisesInteractionByRegion;

public class GetProgramExercisesInteractionByRegionQueryHandler : IRequestHandler<GetProgramExercisesInteractionByRegionQuery, List<ProgramExerciseInteractionMultipleDayModel>>
{
    private readonly IProgramRepository _programRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;
    public GetProgramExercisesInteractionByRegionQueryHandler(IProgramRepository programRepository, IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _programRepository = programRepository;
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }

    public async Task<List<ProgramExerciseInteractionMultipleDayModel>> Handle(GetProgramExercisesInteractionByRegionQuery request, CancellationToken cancellationToken)
    {
        // herhangi bir exercise programda ise programRxercise den gelecek bilgileri sağlayacak
        var programExercises = await _programRepository.GetProgramExercisesAsync(request.ProgramId, cancellationToken);
        // istenen region'lara göre bütün egzersizler programda olsun olmasın sağlanacak
        var exercisesByRegion = await _exerciseRepository.GetExerciseListByRegionAsync(request.RegionIds, cancellationToken);

        var responseModel = exercisesByRegion.Select(e =>
        { 
            var item = new ProgramExerciseInteractionMultipleDayModel()
            {
                Regions = e.RegionExercises != null ? e.RegionExercises.Select(re => _mapper.Map<RegionResponseDto>(re.Region)).ToList() : default,
                Exercise = _mapper.Map<ExerciseResponseDto>(e),
                ProgramExercises = programExercises.Where(pe => pe.ExerciseId == e.Id).Select(pe => _mapper.Map<ProgramExerciseResponseDto>(pe)).ToList(),
            };
            return item;
        }
        ).ToList();

        return responseModel;
    }
}