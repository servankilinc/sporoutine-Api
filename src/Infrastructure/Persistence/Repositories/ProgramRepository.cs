using Application;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.BaseRepository;

namespace Persistence.Repositories;
 
public class ProgramRepository : EFRepositoryBase<Program, AppBaseDbContext>, IProgramRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ProgramRepository(IHttpContextAccessor httpContextAccessor, AppBaseDbContext context) : base(context)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ProgramExercise> AddExerciseToProgramAsync(ProgramExercise programExercise, CancellationToken cancellationToken = default)
    { 
        _context.Entry(programExercise).State = EntityState.Added;
        await _context.SaveChangesAsync(cancellationToken);
        return programExercise;
    }

    public async Task<ProgramExercise> UpdateExerciseOfProgramAsync(ProgramExercise programExercise, CancellationToken cancellationToken = default)
    {
        _context.Entry(programExercise).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return programExercise;
    }

    public async Task RemoveExerciseFromProgramAsync(Guid programExerciseId, CancellationToken cancellationToken = default)
    {
        var temp = new ProgramExercise() {
            Id = programExerciseId, 
        };
        _context.Entry(temp).State = EntityState.Deleted;
        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task ProgramExerciseCompletedAsync(Guid programExerciseId, CancellationToken cancellationToken = default)
    {
        DateTime now = DateTime.UtcNow;
        Fulfillment fulfillment = new()
        {
            ProgramExerciseId = programExerciseId,
            CompletionDateIndexer = DateOnly.FromDateTime(now.GetClientLocalDate()),
            CompletionDate = now
        };

        _context.Entry(fulfillment).State = EntityState.Added;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ProgramExerciseNotCompletedAsync(Guid programExerciseId, CancellationToken cancellationToken = default)
    {
        DateTime now = DateTime.UtcNow.GetClientLocalDate();
        Fulfillment fulfillment = new()
        {
            ProgramExerciseId = programExerciseId, 
            CompletionDateIndexer = DateOnly.FromDateTime(now),
        };

        _context.Entry(fulfillment).State = EntityState.Deleted;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ProgramExercise> GetProgramExerciseAsync(Guid programExerciseId, CancellationToken cancellationToken = default)
    {
        ProgramExercise? data = await _context.ProgramExercises
            .Where(pe => pe.Id == programExerciseId)
            .Include(pe => pe.Exercise)
            .SingleOrDefaultAsync(cancellationToken);
        if (data == null) throw new Exception($"Program exercise could not be found({programExerciseId}).");
        return data;
    }

    public async Task<List<ProgramExercise>> GetProgramExercisesAsync(Guid programId, CancellationToken cancellationToken = default)
    {
        var programExercises = await _context.Set<ProgramExercise>()
            .Where(pe => pe.ProgramId == programId)
            .ToListAsync(cancellationToken);

        return programExercises;
    }

    public async Task<List<ProgramExercise>> GetProgramExercisesByDayAsync(Guid programId, int day, CancellationToken cancellationToken = default)
    {
        var programExercises = await _context.Set<ProgramExercise>()
            .Where(pe => pe.ProgramId == programId && pe.Day == day)
            .Include(pe => pe.Exercise)
            .ToListAsync(cancellationToken);

        return programExercises;
    }

    public async Task<List<Program>> GetProgramsImprovementsByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var programs = await _context.Set<Program>()
            .Where(p => p.UserId == userId)
            .Include(p => p.ProgramExercises)
                .ThenInclude(pe => pe.Exercise)
             .Include(p => p.ProgramExercises)
                .ThenInclude(pe => pe.Fulfillments)
            .ToListAsync(cancellationToken);

        return programs;
    }  

     

    public async Task<List<Program>> GetAllProgramExercisesDetailCurrentDayAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var currentDayIndex = GetDayIndexOfNow();

        var programs = await _context.Programs
        .Where(p => p.UserId == userId) 
        .Where(p => p.ProgramExercises == null ? false : p.ProgramExercises.Any(pe => pe.Day == currentDayIndex)) 
        .Include(p => p.ProgramExercises!.Where(pe => pe.Day == currentDayIndex)) 
            .ThenInclude(pe => pe.Fulfillments) 
        .Include(p => p.ProgramExercises!.Where(pe => pe.Day == currentDayIndex)) 
            .ThenInclude(pe => pe.Exercise) 
        .ToListAsync(cancellationToken);

        return programs;
    }


    // HELPER METHODS
    private int GetDayIndexOfNow()
    {
        int addedDateIndex = 0;
        var now = DateTime.UtcNow;
        switch (now.GetClientLocalDate().DayOfWeek)
        {
            case DayOfWeek.Monday:
                addedDateIndex = 1;
                break;
            case DayOfWeek.Tuesday:
                addedDateIndex = 2;
                break;
            case DayOfWeek.Wednesday:
                addedDateIndex = 3;
                break;
            case DayOfWeek.Thursday:
                addedDateIndex = 4;
                break;
            case DayOfWeek.Friday:
                addedDateIndex = 5;
                break;
            case DayOfWeek.Saturday:
                addedDateIndex = 6;
                break;
            case DayOfWeek.Sunday:
                addedDateIndex = 7;
                break;
            default:
                break;
        }
        return addedDateIndex;
    }

}
