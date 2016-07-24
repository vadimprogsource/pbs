using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Api;
using Core.Security;
using Api.Repository;
using Repository;
using PBS.Models;
using Sys.Helpers;
using Core;
using Web.Middlewares;

namespace PBS
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();


            services.AddScoped<IAuthProvider   , AuthProvider> ();
            services.AddScoped<ISecurityContext, SecurityContext>();


            services.AddTransient<ISecurityService, AuthProvider> ();
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddTransient<IProjectServer, ProjectServer>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddSingleton<IConfig>(x =>  new ConfigModel
            {
                 ConnectionString         = Configuration["Data:DefaultConnection:ConnectionString"],
                 SessionExpirationTimeout = TimeSpanHelper.Parse(Configuration["Security:SessionTimeout"]),
                 AuthAccessOnly           = new HashSet<string>(Configuration.GetSection("Security:MembersAccessRoots").GetChildren().Select(x1=>x1.Value.ToLowerInvariant()))
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            app.UseAuth()
               .UseStaticFiles()
               .UseMvc();
                 
        }
    }
}
