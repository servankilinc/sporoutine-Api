using Application.Services.Repositories;
using MediatR;

namespace Application.CQRS.Region_.Commands.Delete;

public class RegionDeleteCommandHandler : IRequestHandler<RegionDeleteCommand>
{
    private readonly IRegionRepository _regionRepository;
    public RegionDeleteCommandHandler(IRegionRepository regionRepository) => _regionRepository = regionRepository;

    public async Task Handle(RegionDeleteCommand request, CancellationToken cancellationToken)
    {
        await _regionRepository.DeleteByFilterAsync(filter: f => f.Id == request.Id, cancellationToken);
    }
}