using Application.CQRS.Exercise_.Commands.AddRegionToExercise;
using Application.CQRS.Exercise_.Commands.RemoveRegionFromExercise;
using Application.Services.Repositories.BaseRepositories;
using Domain.Entities;
using Domain.Models.Pagination;

namespace Application.Services.Repositories;

public interface IExerciseRepository : IRepository<Exercise>, IRepositoryAsync<Exercise>
{
    Task<Paginate<Exercise>> GetAllExerciseList(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<List<ProgramExercise>> GetExerciseListByProgramAsync(Guid programId, CancellationToken cancellationToken = default);
    Task<List<Exercise>> GetExerciseListByRegionAsync(Guid[] regionIds, CancellationToken cancellationToken = default);

    Task AddRegionToExerciseAsync(AddRegionToExerciseCommand addRegionToExerciseCommand, CancellationToken cancellationToken = default);
    Task RemoveRegionFromExerciseAsync(RemoveRegionFromExerciseCommand removeRegionFromExerciseCommand, CancellationToken cancellationToken = default);
}
