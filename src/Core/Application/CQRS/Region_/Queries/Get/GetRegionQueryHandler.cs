using Application.CQRS.Region_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Region_.Queries.Get;

public class GetRegionQueryHandler : IRequestHandler<GetRegionQuery, RegionResponseDto>
{
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;
    public GetRegionQueryHandler(IRegionRepository regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    public async Task<RegionResponseDto> Handle(GetRegionQuery request, CancellationToken cancellationToken)
    {
        Region region = await _regionRepository.GetAsync(filter: f => f.Id == request.Id, cancellationToken: cancellationToken);
        return _mapper.Map<RegionResponseDto>(region);
    }
}
