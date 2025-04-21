using Application.Services.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IProgramRepository : IRepository<Program>, IRepositoryAsync<Program>
{
    // Exercise Program Interaction Methods...
    Task<ProgramExercise> AddExerciseToProgramAsync(ProgramExercise programExercise, CancellationToken cancellationToken = default);
    Task<ProgramExercise> UpdateExerciseOfProgramAsync(ProgramExercise programExercise, CancellationToken cancellationToken = default);
    Task RemoveExerciseFromProgramAsync(Guid programExerciseId, CancellationToken cancellationToken = default);
    // Exercise Completion Methods...
    Task ProgramExerciseCompletedAsync(Guid programExerciseId, CancellationToken cancellationToken = default);
    Task ProgramExerciseNotCompletedAsync(Guid programExerciseId, CancellationToken cancellationToken = default);

    // ProgramExercise Queries...
    /// <summary>
    /// Istenen ProgramExercise bilgisini ham haliyle verir
    /// </summary>
    /// <param name="programExerciseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ProgramExercise> GetProgramExerciseAsync(Guid programExerciseId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Istenen programın egzersizlerini ham haliyle verir
    /// </summary>
    /// <param name="programId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<ProgramExercise>> GetProgramExercisesAsync(Guid programId, CancellationToken cancellationToken = default);
    /// <summary>
    /// Bir programın egzersizlerini istenen gün için verir
    /// </summary>
    /// <param name="programId"></param>
    /// <param name="day"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<ProgramExercise>> GetProgramExercisesByDayAsync(Guid programId, int day, CancellationToken cancellationToken = default);

    // Complex Queries...
    /// <summary>
    /// Kullancının programlarındaki ilerleme bilgilerini verir
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Program>> GetProgramsImprovementsByUserAsync(Guid userId, CancellationToken cancellationToken = default); 
    /// <summary>
    /// Kullanıcını programlarının hepsinin o gün için detayını kullanıcının uygulama bilgieri ile verir
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Program>> GetAllProgramExercisesDetailCurrentDayAsync(Guid userId, CancellationToken cancellationToken = default);

}
