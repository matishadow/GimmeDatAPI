using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GimmeDatAPI.Configuration;
using GimmeDatAPI.Configuration.InversionOfControl;
using GimmeDatAPI.PlainOldClrObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GimmeDatAPI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            SwaggerConfiguration.RegisterService(services);
            CorsConfiguration.Register(services,
                Configuration.GetSection(nameof(CorsConfigurationValues)).Get<CorsConfigurationValues>());
            
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            InversionOfControlConfiguration.Register(containerBuilder);
            IContainer container = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(container);
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            InversionOfControlConfiguration.Register(builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }
            
            applicationBuilder.UseCors(CorsConfiguration.CorsPolicyName);
            
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            applicationBuilder.UseRewriter(option);

            applicationBuilder.UseMvc(routes =>
            {
                routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
            
            applicationBuilder.UseSwagger();
            SwaggerConfiguration.RegisterUi(applicationBuilder);
        }
    }
}