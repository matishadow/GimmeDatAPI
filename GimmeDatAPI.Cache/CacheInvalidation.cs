using System;
using GimmeDatAPI.Configuration.InversionOfControl.RegistrationRelated;
using GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated;
using NodaTime;
using NodaTime.TimeZones;

namespace GimmeDatAPI.Cache
{
    public class CacheInvalidation : ICacheInvalidation,
        IInstancePerLifetimeScopeDependency, IAsImplementedInterfacesDependency
    {
        private const string ZascianekTimeZone = "Europe/Warsaw";
        private readonly LocalTime menuUpdateTime = new LocalTime(19, 0, 0);
        
        public TimeSpan GetZascianekExpireAfter()
        {
            DateTime currentDateTimeUtc = DateTime.UtcNow;
            DateTimeZone zascianekTimeZone = DateTimeZoneProviders.Tzdb[ZascianekTimeZone];

            var currentDateTime = new ZonedDateTime(Instant.FromDateTimeUtc(currentDateTimeUtc), zascianekTimeZone);
            ZonedDateTime menuDateTime = GetNextMenuUpdateDateTime(currentDateTime, zascianekTimeZone);

            Duration expireAfter = menuDateTime - currentDateTime;

            return expireAfter.ToTimeSpan();
        }

        private ZonedDateTime GetNextMenuUpdateDateTime(ZonedDateTime currentDateTime, DateTimeZone timeZone)
        {
            LocalDate menuLocalDate = currentDateTime.Date;

            LocalDateTime menuLocalDateTime = currentDateTime.TimeOfDay > menuUpdateTime
                ? menuLocalDate.PlusDays(1).At(menuUpdateTime)
                : menuLocalDate.At(menuUpdateTime);
            
            return menuLocalDateTime.InZoneLeniently(timeZone);
        }
    }
}