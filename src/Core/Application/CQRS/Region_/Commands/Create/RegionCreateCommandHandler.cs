using Application.CQRS.Region_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Region_.Commands.Create;

public class RegionCreateCommandHandler : IRequestHandler<RegionCreateCommand, RegionResponseDto>
{
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;
    public RegionCreateCommandHandler(IRegionRepository regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    public async Task<RegionResponseDto> Handle(RegionCreateCommand request, CancellationToken cancellationToken)
    {
        Region regionToInsert = _mapper.Map<Region>(request);
        Region insertedRegion = await _regionRepository.AddAsync(regionToInsert, cancellationToken);
        return _mapper.Map<RegionResponseDto>(insertedRegion);
    }
}