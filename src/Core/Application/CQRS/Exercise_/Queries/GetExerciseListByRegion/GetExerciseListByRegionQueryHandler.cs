using Application.CQRS.Exercise_.Dtos;
using Application.CQRS.Exercise_.Models;
using Application.CQRS.Region_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Exercise_.Queries.GetExerciseListByRegion;

public class GetExerciseListByRegionQueryHandler : IRequestHandler<GetExerciseListByRegionQuery, List<ExerciseResponseModel>>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;
    public GetExerciseListByRegionQueryHandler(IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }
    public async Task<List<ExerciseResponseModel>> Handle(GetExerciseListByRegionQuery request, CancellationToken cancellationToken)
    {
        if (request.RegionIds == null) throw new ArgumentNullException("Region ID list should be filled!");
        if (request.RegionIds.Length == 0) throw new ArgumentException("Region ID list should be filled!");

        List<Exercise> result = await _exerciseRepository.GetExerciseListByRegionAsync(request.RegionIds!, cancellationToken);

        return result.Select(e => new ExerciseResponseModel()
        {
            Exercise = _mapper.Map<ExerciseResponseDto>(e),
            Regions = e.RegionExercises != null ? e.RegionExercises.Select(re => _mapper.Map<RegionResponseDto>(re.Region)).ToList() : default(List<RegionResponseDto>),
        }).ToList();
    }
}