using Application.CQRS.Exercise_.Dtos;
using Application.CQRS.Exercise_.Models;
using Application.CQRS.Region_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Exercise_.Queries.GetAllExerciseList;

public class GetAllExerciseListQueryHandler : IRequestHandler<GetAllExerciseListQuery, Paginate<ExerciseResponseModel>>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IMapper _mapper;
    public GetAllExerciseListQueryHandler(IExerciseRepository exerciseRepository, IMapper mapper)
    {
        _exerciseRepository = exerciseRepository;
        _mapper = mapper;
    }
    public async Task<Paginate<ExerciseResponseModel>> Handle(GetAllExerciseListQuery request, CancellationToken cancellationToken)
    {
        //Paginate<Exercise> result = await _exerciseRepository.GetPaginatedListAsync(index: request.PagingRequest!.Page, size: request.PagingRequest.PageSize);

        Paginate<Exercise> result = await _exerciseRepository.GetAllExerciseList(request.PagingRequest!.Page, request.PagingRequest.PageSize, cancellationToken);

        //Paginate<Exercise> result2 = await _exerciseRepository.GetPaginatedListAsync(index: request.PagingRequest!.Page,size: request.PagingRequest.PageSize,
        //    include: f => f.Include(x => x.RegionExercises).ThenInclude(re => re.Region), cancellationToken: cancellationToken);
        
        var mappedItems = result.Items.Select(e => new ExerciseResponseModel()
        {
            Exercise = _mapper.Map<ExerciseResponseDto>(e),
            Regions = e.RegionExercises != null ? e.RegionExercises.Select(re => _mapper.Map<RegionResponseDto>(re.Region)).ToList() : default(List<RegionResponseDto>),
        }).ToList();

        return new Paginate<ExerciseResponseModel>()
        {
            Items = mappedItems,
            Count = result.Count,
            Index = result.Index,
            Pages = result.Pages,
            Size = result.Size,
        };
    }
}