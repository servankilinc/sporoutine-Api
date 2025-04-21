using Application.Services.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.BaseRepository; 

namespace Persistence.Repositories;

public class UserRepository : EFRepositoryBase<User, AppBaseDbContext>, IUserRepository
{
    public UserRepository(AppBaseDbContext context) : base(context)
    {
    }
}
