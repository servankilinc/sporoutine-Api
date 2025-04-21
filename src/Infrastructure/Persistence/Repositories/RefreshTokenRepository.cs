using Application.Services.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.BaseRepository;

namespace Persistence.Repositories;

public class RefreshTokenRepository : EFRepositoryBase<RefreshToken, AppBaseDbContext>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppBaseDbContext context) : base(context)
    {
    }
}
