using Application.CQRS.Region_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Region_.Queries.GetAll;

public class GetAllRegionQueryHandler : IRequestHandler<GetAllRegionQuery, List<RegionResponseDto>>
{
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;
    public GetAllRegionQueryHandler(IRegionRepository regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    public async Task<List<RegionResponseDto>> Handle(GetAllRegionQuery request, CancellationToken cancellationToken)
    {
        var list = await _regionRepository.GetAllAsync(cancellationToken: cancellationToken);
        return list.Select(r => _mapper.Map<RegionResponseDto>(r)).ToList();
    }
}
