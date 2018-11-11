using FluentCache;
using GimmeDatAPI.Configuration.InversionOfControl.RegistrationRelated;
using GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated;
using IdentityServer4.Services;

namespace GimmeDatAPI.Cache
{
    public class GimmeDatCache : IGimmeDatCache,
        ISingleInstanceDependency, IAsImplementedInterfacesDependency
    {
        private readonly ICache cacheDictionary;

        public GimmeDatCache()
        {
            cacheDictionary = new FluentCache.Simple.FluentDictionaryCache();
        }

        public Cache<T> GetCacheWrapper<T>(T repository) 
        {
            return cacheDictionary.WithSource(repository);
        }
    }
}