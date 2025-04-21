using Application.CQRS.Exercise_.Commands.AddRegionToExercise;
using Application.CQRS.Exercise_.Commands.RemoveRegionFromExercise;
using Application.Services.Repositories;
using Domain.Entities;
using Domain.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Extensions;
using Persistence.Repositories.BaseRepository;

namespace Persistence.Repositories;

public class ExerciseRepository : EFRepositoryBase<Exercise, AppBaseDbContext>, IExerciseRepository
{
    public ExerciseRepository(AppBaseDbContext context) : base(context)
    {
    }

    public async Task<Paginate<Exercise>> GetAllExerciseList(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var exercises = await _context.Exercises
            .Include(e => e.RegionExercises)
                .ThenInclude(re => re.Region)
            .ToPaginateAsync(page, pageSize, cancellationToken);

        return exercises!;
    }


    public async Task<List<ProgramExercise>> GetExerciseListByProgramAsync(Guid programId, CancellationToken cancellationToken = default)
    {
        var exercises = await _context.ProgramExercises
            .Where(pe => pe.ProgramId == programId)
            .Include(pe => pe.Exercise)
                .ThenInclude(e => e.RegionExercises)
                    .ThenInclude(re => re.Region)
            .ToListAsync(cancellationToken);

        return exercises!;
    }

    public async Task<List<Exercise>> GetExerciseListByRegionAsync(Guid[] regionIds, CancellationToken cancellationToken = default)
    {
        var exercises = await _context.Exercises
            .Where(e => e.RegionExercises != null && e.RegionExercises.Any(re => regionIds.Contains(re.RegionId)))
            .Include(e => e.RegionExercises!.Where(re => re != null))
                .ThenInclude(re => re.Region)
            .ToListAsync(cancellationToken);

        return exercises!;
    }


    public async Task AddRegionToExerciseAsync(AddRegionToExerciseCommand addRegionToExerciseCommand, CancellationToken cancellationToken = default)
    {
        RegionExercise temp = new()
        {
            ExerciseId = addRegionToExerciseCommand.ExerciseId,
            RegionId = addRegionToExerciseCommand.RegionId,
        };

        _context.Entry(temp).State = EntityState.Added;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveRegionFromExerciseAsync(RemoveRegionFromExerciseCommand removeRegionFromExerciseCommand, CancellationToken cancellationToken = default)
    {
        var temp = new RegionExercise()
        { 
            ExerciseId = removeRegionFromExerciseCommand.ExerciseId,
            RegionId = removeRegionFromExerciseCommand.RegionId,
        };
        _context.Entry(temp).State = EntityState.Deleted;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
