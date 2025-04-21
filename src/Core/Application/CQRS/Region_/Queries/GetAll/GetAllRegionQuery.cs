using Application.CQRS.Region_.Dtos;
using MediatR;

namespace Application.CQRS.Region_.Queries.GetAll;

public class GetAllRegionQuery : IRequest<List<RegionResponseDto>>
{
}
