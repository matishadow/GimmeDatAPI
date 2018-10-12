using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace GimmeDatAPI.Configuration
{
    public class SwaggerConfiguration
    {
        private const string SwaggerUiEndpoint = "/swagger/v1/swagger.json";
        private const string ApiVersion = "v1";
        private const string XmlExtension = ".xml";
        private const string AuthorizationType = "Bearer";
        private const string ApiKeySchemeName = "Authorization";
        private const string ApiKeySchemeIn = "header";
        private const string ApiKeySchemeType = "apiKey";
        private const string ApplicationName = "GimmeDatAPI.Web";
        
        private const string ApiTitle = "GimmeDat API";
        private const string ApiDescription = "API for GimmeDat project";

        public static void RegisterService(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersion, new Info {Title = ApiTitle, Version = ApiVersion});
                c.AddSecurityDefinition(AuthorizationType, new ApiKeyScheme
                {
                    Description = ApiDescription,
                    Name = ApiKeySchemeName,
                    In = ApiKeySchemeIn,
                    Type = ApiKeySchemeType
                });

                c.DescribeAllEnumsAsStrings();
                c.IgnoreObsoleteProperties();
                c.IncludeXmlComments(CreateXmlCommentsPath());
            });
        }

        private static string CreateXmlCommentsPath()
        {
            const string xmlCommentsFileName = ApplicationName + XmlExtension;

            string basePath = PlatformServices.Default.Application.ApplicationBasePath;
            string xmlPath = Path.Combine(basePath, xmlCommentsFileName);

            return xmlPath;
        }

        public static void RegisterUi(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerUiEndpoint, ApiDescription);
            });
        }
    }
}