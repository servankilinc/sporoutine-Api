using Microsoft.EntityFrameworkCore;
using Application.GlobalExceptionHandler.CustomExceptions;
using Application.Services.Repositories;
using Persistence.Repositories.BaseRepository;
using Persistence.Contexts;
using Domain.Entities;

namespace Persistence.Repositories;

public class PhysicalDataRepository : EFRepositoryBase<PhysicalData, AppBaseDbContext>, IPhysicalDataRepository
{
    public PhysicalDataRepository(AppBaseDbContext context) : base(context)
    {
    }

    public async Task<List<WeightHistoryData>> GetAllWeightHistoryDataByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        List<WeightHistoryData> weightDataHistoryList = await _context.Set<WeightHistoryData>().Where(d => d.UserId == userId).ToListAsync(cancellationToken);
        return weightDataHistoryList;
    }

    public async Task<WeightHistoryData> GetCurrentDayWeightHistoryDataAsync(Guid userId, DateTime currentDate, CancellationToken cancellationToken = default)
    { 
        WeightHistoryData? weightDataHistory = await _context.Set<WeightHistoryData>().Where(d => d.UserId == userId &&  d.AddedDate.Date == currentDate.Date).FirstOrDefaultAsync(cancellationToken);
        return weightDataHistory!;
    }

    public async Task AddNewWeightHistoryDataAsync(WeightHistoryData weightData, CancellationToken cancellationToken = default)
    {
        _context.Set<WeightHistoryData>().Add(weightData);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateWeightHistoryDataAsync(WeightHistoryData weightData, CancellationToken cancellationToken = default)
    {
        _context.Set<WeightHistoryData>().Update(weightData);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteWeightHistoryDataAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var weightDataHistory = await _context.Set<WeightHistoryData>().Where(d => d.Id == id).FirstOrDefaultAsync(cancellationToken);
        if (weightDataHistory == null) throw new BusinessException("The data could not found to delete.");
        _context.Remove(weightDataHistory);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
