using Application.Services.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IPhysicalDataRepository : IRepository<PhysicalData>, IRepositoryAsync<PhysicalData>
{
    Task<List<WeightHistoryData>> GetAllWeightHistoryDataByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<WeightHistoryData> GetCurrentDayWeightHistoryDataAsync(Guid userId, DateTime currentDate, CancellationToken cancellationToken = default);
    Task AddNewWeightHistoryDataAsync(WeightHistoryData weightData, CancellationToken cancellationToken = default);
    Task UpdateWeightHistoryDataAsync(WeightHistoryData weightData, CancellationToken cancellationToken = default);
    Task DeleteWeightHistoryDataAsync(Guid id, CancellationToken cancellationToken = default);
}