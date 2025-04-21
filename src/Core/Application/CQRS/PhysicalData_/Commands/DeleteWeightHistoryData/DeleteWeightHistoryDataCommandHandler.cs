using Application.Services.Repositories;
using MediatR;

namespace Application.CQRS.PhysicalData_.Commands.DeleteWeightHistoryData;

public class DeleteWeightHistoryDataCommandHandler : IRequestHandler<DeleteWeightHistoryDataCommand>
{
    private readonly IPhysicalDataRepository _physicalDataRepository;
    public DeleteWeightHistoryDataCommandHandler(IPhysicalDataRepository physicalDataRepository) => _physicalDataRepository = physicalDataRepository;

    public async Task Handle(DeleteWeightHistoryDataCommand request, CancellationToken cancellationToken)
    {
        await _physicalDataRepository.DeleteWeightHistoryDataAsync(request.Id, cancellationToken);
    } 
}