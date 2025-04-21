using Application.Services.Repositories.BaseRepositories;
using Domain.Entities;
using Domain.Models.DynamicQuery;
using Domain.Models.Pagination;
using Domain.Models.Signings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.Extensions;
using System.Linq.Expressions;

namespace Persistence.Repositories.BaseRepository;

public class EFRepositoryBase<TEntity, TContext> : IRepository<TEntity>, IRepositoryAsync<TEntity> 
    where TEntity : class, IEntity 
    where TContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    protected TContext _context { get; set; }
    public EFRepositoryBase(TContext context) => _context = context;
    


    // ************* SYNC PROCESSES **************
    public TEntity Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (include != null) queryable = include(queryable);
        return queryable.FirstOrDefault(filter)!;
    }

    public TEntity Add(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Added;
        _context.SaveChanges();
        return entity;
    }

    public void Delete(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public TEntity Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
        return entity;
    }

    public void DeleteByFilter(Expression<Func<TEntity, bool>> filter)
    {
        var entity = _context.Set<TEntity>().Where(filter).FirstOrDefault();
        if (entity == null) throw new InvalidOperationException("The specified entity to delete could not be found.");
        _context.Entry(entity).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public bool IsExist(Expression<Func<TEntity, bool>> filter)
    {
        return _context.Set<TEntity>().Any(filter);
    }

    public ICollection<TEntity> GetAll(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (filter != null) queryable = queryable.Where(filter);
        if (orderBy != null)
            return orderBy(queryable).ToList();
        return queryable.ToList();
    }
     

    public Paginate<TEntity> GetPaginatedList(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = default,
        int size = default,
        bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (filter != null) queryable = queryable.Where(filter);
        if (orderBy != null)
            return orderBy(queryable).ToPaginate(index, size);
        return queryable.ToPaginate(index, size);
    }

    public ICollection<TEntity> GetAllByDynamic(
        DynamicQuery dynamicQuery,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>().AsQueryable().ToDynamic(dynamicQuery);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return queryable.ToList();
    }

    public Paginate<TEntity> GetPaginatedListByDynamic(
        DynamicQuery dynamicQuery,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = default,
        int size = default,
        bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>().AsQueryable().ToDynamic(dynamicQuery);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return queryable.ToPaginate(index, size);
    }


    // ************* ASYNC PROCESSES **************

    public async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        var result = await queryable.FirstOrDefaultAsync(filter, cancellationToken);
        return result!;
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Added;
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteByFilterAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
        if (entity == null) throw new InvalidOperationException("The specified entity to delete could not be found.");
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AnyAsync(filter, cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (filter != null) queryable = queryable.Where(filter);
        if (orderBy != null)
            return await orderBy(queryable).ToListAsync(cancellationToken);
        return await queryable.ToListAsync(cancellationToken);
    }

    public async Task<Paginate<TEntity>> GetPaginatedListAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = default,
        int size = default,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (filter != null) queryable = queryable.Where(filter);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(index, size, cancellationToken);
        return await queryable.ToPaginateAsync(index, size, cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetAllByDynamicAsync(
        DynamicQuery dynamicQuery,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>().AsQueryable().ToDynamic(dynamicQuery);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return await queryable.ToListAsync(cancellationToken);
    }

    public async Task<Paginate<TEntity>> GetPaginatedListByDynamicAsync(
        DynamicQuery dynamicQuery,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = default,
        int size = default,
        bool enableTracking = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = _context.Set<TEntity>().AsQueryable().ToDynamic(dynamicQuery);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        return await queryable.ToPaginateAsync(index, size, cancellationToken);
    }
}
