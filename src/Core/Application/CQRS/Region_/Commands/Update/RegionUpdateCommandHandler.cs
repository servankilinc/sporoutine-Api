using Application.CQRS.Region_.Dtos;
using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Region_.Commands.Update;

public class RegionUpdateCommandHandler : IRequestHandler<RegionUpdateCommand, RegionResponseDto>
{
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;
    public RegionUpdateCommandHandler(IRegionRepository regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    public async Task<RegionResponseDto> Handle(RegionUpdateCommand request, CancellationToken cancellationToken)
    {
        Region existRegion =  await _regionRepository.GetAsync(filter: f => f.Id == request.Id, cancellationToken: cancellationToken);
        if (existRegion == null) throw new BusinessException("The region could not be found to update");

        Region regionToUpdate = _mapper.Map(request, existRegion);
        Region updatedRegion = await _regionRepository.UpdateAsync(regionToUpdate, cancellationToken: cancellationToken);
        return _mapper.Map<RegionResponseDto>(updatedRegion);
    }
}