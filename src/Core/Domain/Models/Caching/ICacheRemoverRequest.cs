namespace Domain.Models.Caching;

public interface ICacheRemoverRequest
{
    string CacheKey { get; }
    List<string> CacheGroupKeys { get; }
}
