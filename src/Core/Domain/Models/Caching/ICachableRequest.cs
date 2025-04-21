namespace Domain.Models.Caching;

public interface ICachableRequest
{
    string CacheKey { get; }
    List<string> CacheGroupKeys { get; }
    bool BypassCache { get; }
    TimeSpan? SlidingExpiration { get; }
    DateTime? AbsoluteExpiration { get; }
}