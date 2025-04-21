using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.PhysicalData_.Commands.SaveOrUpdateWeight;

public class SaveOrUpdateWeightCommandHandler : IRequestHandler<SaveOrUpdateWeightCommand>
{
    private readonly IPhysicalDataRepository _physicalDataRepository;
    public SaveOrUpdateWeightCommandHandler(IPhysicalDataRepository physicalDataRepository) => _physicalDataRepository = physicalDataRepository;

    public async Task Handle(SaveOrUpdateWeightCommand request, CancellationToken cancellationToken)
    {
        PhysicalData existData = await _physicalDataRepository.GetAsync(filter: p => p.UserId == request.UserId);
        if (existData != null)
        {
            await SaveWeightHistoryData(request, cancellationToken);

            existData.Weight = request.Weight;
            await _physicalDataRepository.UpdateAsync(existData, cancellationToken);
        }
        else
        {
            await SaveWeightHistoryData(request, cancellationToken);

            PhysicalData dataToInsert = new()
            {
                UserId = request.UserId,
                Weight = request.Weight,
            };
            await _physicalDataRepository.AddAsync(dataToInsert, cancellationToken);
        }
    }

    private async Task SaveWeightHistoryData(SaveOrUpdateWeightCommand request, CancellationToken cancellationToken)
    {
        var currentDayWeight = await _physicalDataRepository.GetCurrentDayWeightHistoryDataAsync(request.UserId, DateTime.UtcNow.GetClientLocalDate(), cancellationToken);
        if (currentDayWeight == null)
        {
            WeightHistoryData weightData = new()
            {
                UserId = request.UserId,
                AddedDate = DateTime.UtcNow.GetClientLocalDate(),
                Weight = request.Weight
            };
            await _physicalDataRepository.AddNewWeightHistoryDataAsync(weightData, cancellationToken);
        }
        else
        {
            currentDayWeight.Weight = request.Weight;
            await _physicalDataRepository.UpdateWeightHistoryDataAsync(currentDayWeight, cancellationToken);
        }
    }
}