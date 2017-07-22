using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace ExploreCalifornia
{
    public class Startup
    {
        private readonly IConfigurationRoot configuration;

        public Startup(IHostingEnvironment env) {
            configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile(env.ContentRootPath + "/config.json")
                .AddJsonFile(env.ContentRootPath + "/config.development.json", true)
                .Build();
        }

        // Our IOC Container (Inversion Of Control)
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /*
             ASP.NET Core allows you to completely control how instances are created by allowing you to pass the AddTransient
             method a function, that returns an instance of type. For example, 
             I can create my own instance of the FeatureToggles class, like this.
             */
            services.AddTransient<FeatureToggles>(x => new FeatureToggles{
                EnableDeveloperExceptions = configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions")
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, FeatureToggles feature)
        {
            loggerFactory.AddConsole();

            // telling ASP.NET Core's configuration library about the various places that the application's configuration settings will be stored. 
            // true for optional
            

            app.UseExceptionHandler("/error.html");

            //use the configuration object just like a dictionary.
            if (feature.EnableDeveloperExceptions)
            {
                app.UseDeveloperExceptionPage();
            }

            //Generate Error for testing
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("Error");
                await next();
            });

            app.UseMvc(routes => {
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseFileServer();
        }
    }
}
