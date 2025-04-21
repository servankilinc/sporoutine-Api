using Domain.Models.Caching;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Application.Pipelines.Caching;

public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICacheRemoverRequest
{
    private readonly IDistributedCache _cache;
    public CacheRemovingBehavior(IDistributedCache cache) => _cache = cache;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = await next();

        if (!string.IsNullOrEmpty(request.CacheKey))
        {
            await _cache.RemoveAsync(request.CacheKey, cancellationToken);
        }

        if (request.CacheGroupKeys.Any())
        {
            await DeleteCacheKeysByGroups(request, cancellationToken);
        }

        return response;
    }

    private async Task DeleteCacheKeysByGroups(TRequest request, CancellationToken cancellationToken)
    {
        foreach (string cacheGroupKey in request.CacheGroupKeys)
        {
            byte[]? keyListFromCache = await _cache.GetAsync(cacheGroupKey, cancellationToken);
            await _cache.RemoveAsync(cacheGroupKey, cancellationToken);
            if (keyListFromCache == null) continue;

            string stringKeyList = Encoding.Default.GetString(keyListFromCache);
            HashSet<string>? keyListInGroup = JsonConvert.DeserializeObject<HashSet<string>>(stringKeyList);
            if (keyListInGroup == null) continue;

            foreach (var key in keyListInGroup)
            {
                await _cache.RemoveAsync(key, cancellationToken);
            }
        }
    }
}
