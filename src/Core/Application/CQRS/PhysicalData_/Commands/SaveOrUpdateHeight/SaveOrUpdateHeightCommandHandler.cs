using Application.Services.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.PhysicalData_.Commands.SaveOrUpdateHeight;

public class SaveOrUpdateHeightCommandHandler : IRequestHandler<SaveOrUpdateHeightCommand>
{
    private readonly IPhysicalDataRepository _physicalDataRepository;
    public SaveOrUpdateHeightCommandHandler(IPhysicalDataRepository physicalDataRepository) => _physicalDataRepository = physicalDataRepository;

    public async Task Handle(SaveOrUpdateHeightCommand request, CancellationToken cancellationToken)
    {
        PhysicalData existData = await _physicalDataRepository.GetAsync(filter: p => p.UserId == request.UserId);
        if (existData != null)
        {
            existData.Height = request.Height;
            await _physicalDataRepository.UpdateAsync(existData, cancellationToken);
        }
        else
        {
            PhysicalData dataToInsert = new()
            {
                UserId = request.UserId,
                Height = request.Height,
            };
            await _physicalDataRepository.AddAsync(dataToInsert, cancellationToken);
        }
    }
}