using Application.CQRS.PhysicalData_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.PhysicalData_.Queries.GetPhysicalData;

public class GetPhysicalDataQueryHandler : IRequestHandler<GetPhysicalDataQuery, PhysicalDataResponseDto>
{
    private readonly IPhysicalDataRepository _physicalDataRepository;
    private readonly IMapper _mapper;

    public GetPhysicalDataQueryHandler(IPhysicalDataRepository physicalDataRepository, IMapper mapper)
    {
        _physicalDataRepository = physicalDataRepository;
        _mapper = mapper;
    }

    public async Task<PhysicalDataResponseDto> Handle(GetPhysicalDataQuery request, CancellationToken cancellationToken)
    {
        var data = await _physicalDataRepository.GetAsync(filter: p => p.UserId == request.UserId, cancellationToken: cancellationToken);
        return _mapper.Map<PhysicalDataResponseDto>(data);
    }
}
