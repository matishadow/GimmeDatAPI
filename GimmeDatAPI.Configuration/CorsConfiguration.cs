using Microsoft.Extensions.DependencyInjection;

namespace GimmeDatAPI.Configuration
{
    public class CorsConfiguration
    {
        public const string CorsPolicyName = "Default";

        public static void Register(IServiceCollection services,
            CorsConfigurationValues configuration)
        {
            services?.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    builder => builder
                        .WithOrigins(configuration.AllowedOrigin)
                        .WithMethods(configuration.AllowedMethods)
                        .WithHeaders(configuration.AllowedHeaders));
            });
        }
    }
}