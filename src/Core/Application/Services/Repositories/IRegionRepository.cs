using Application.Services.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IRegionRepository : IRepository<Region>, IRepositoryAsync<Region>
{
}