using System;

namespace GimmeDatAPI.Cache
{
    public interface ICacheInvalidation
    {
        TimeSpan GetZascianekExpireAfter();
    }
}