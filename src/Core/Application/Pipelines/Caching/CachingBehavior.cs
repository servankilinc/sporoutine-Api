using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using MediatR;
using Domain.Models.Caching;
using Newtonsoft.Json;

namespace Application.Pipelines.Caching;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly IDistributedCache _distributedCache;
    public CachingBehavior(IDistributedCache distributedCache) => _distributedCache = distributedCache;
    

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache) return await next();
        
        TResponse? response;

        byte[]? cachedData = await _distributedCache.GetAsync(request.CacheKey, cancellationToken);
        if (cachedData != null)
        {            
            response = JsonConvert.DeserializeObject<TResponse>(Encoding.UTF8.GetString(cachedData));
            if (response != null) return response;
        }

        response = await next();
        
        DistributedCacheEntryOptions cacheEntryOptions = new() {
            SlidingExpiration = request.SlidingExpiration.HasValue ? request.SlidingExpiration : TimeSpan.FromDays(2), 
            AbsoluteExpiration = request.AbsoluteExpiration.HasValue ? request.AbsoluteExpiration : DateTime.Now.AddDays(6)
        };
        byte[]? serializedResponse = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
        await _distributedCache.SetAsync(request.CacheKey, serializedResponse, cacheEntryOptions, cancellationToken);
            
        if(request.CacheGroupKeys.Any()) await AddCacheKeyToGroups(request, cacheEntryOptions, cancellationToken);
        
        return response;
    }

    private async Task AddCacheKeyToGroups(TRequest request, DistributedCacheEntryOptions cacheEntryOptions, CancellationToken cancellationToken)
    {
        foreach (string cacheGroupKey in request.CacheGroupKeys)
        {
            HashSet<string>? keyListInGroup;
            byte[]? cachedGroupData = await _distributedCache.GetAsync(cacheGroupKey, cancellationToken);
            if (cachedGroupData != null)
            {
                keyListInGroup = JsonConvert.DeserializeObject<HashSet<string>>(Encoding.Default.GetString(cachedGroupData));
                if(keyListInGroup != null && !keyListInGroup.Contains(request.CacheKey))
                {
                    keyListInGroup.Add(request.CacheKey);
                }
            }
            else
            {
                keyListInGroup = new HashSet<string>(new[] { request.CacheKey });                       
            }
            string serializedData = JsonConvert.SerializeObject(keyListInGroup);
            byte[]? newCacheGroupData = Encoding.UTF8.GetBytes(serializedData);

            await _distributedCache.SetAsync(cacheGroupKey, newCacheGroupData, cacheEntryOptions, cancellationToken);
        }
    }
}
