using Application.Services.Repositories.BaseRepositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken>, IRepositoryAsync<RefreshToken>
{
}
