using Application.Services.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.BaseRepository;

namespace Persistence.Repositories;

public class RegionRepository : EFRepositoryBase<Region, AppBaseDbContext>, IRegionRepository
{
    public RegionRepository(AppBaseDbContext context) : base(context)
    {
    }

}