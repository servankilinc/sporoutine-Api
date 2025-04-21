using Application.CQRS.PhysicalData_.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.CQRS.PhysicalData_.Queries.GetWeightHistoryDataByUser;

public class GetWeightHistoryDataByUserQueryHandler : IRequestHandler<GetWeightHistoryDataByUserQuery, List<WeightHistoryDataResponseDto>>
{
    private readonly IPhysicalDataRepository _physicalDataRepository;
    private readonly IMapper _mapper;

    public GetWeightHistoryDataByUserQueryHandler(IPhysicalDataRepository physicalDataRepository, IMapper mapper)
    {
        _physicalDataRepository = physicalDataRepository;
        _mapper = mapper;
    }

    public async Task<List<WeightHistoryDataResponseDto>> Handle(GetWeightHistoryDataByUserQuery request, CancellationToken cancellationToken)
    {
        var dataList = await _physicalDataRepository.GetAllWeightHistoryDataByUserAsync(request.UserId, cancellationToken);

        return dataList.Select(_mapper.Map<WeightHistoryDataResponseDto>).ToList(); ;
    }
}
