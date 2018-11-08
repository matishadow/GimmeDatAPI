using FluentCache;

namespace GimmeDatAPI.Cache
{
    public interface IGimmeDatCache
    {
        Cache<T> GetCacheWrapper<T>(T repository);
    }
}