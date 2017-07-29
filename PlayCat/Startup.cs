using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlayCat.DataService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;
using PlayCat.Music;

namespace PlayCat
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddOptions();
            services.Configure<FolderOptions>(x =>
            {
                IConfigurationSection section = Configuration.GetSection("FolderPaths");
                x.AudioFolderPath = HostingEnvironment.ContentRootPath + section.GetValue<string>("AudioFolderPath");
                x.VideoFolderPath = HostingEnvironment.ContentRootPath + section.GetValue<string>("VideoFolderPath");
            });
            services.Configure<AudioOptions>(Configuration.GetSection("AudioInfo"));
            services.Configure<VideoRestrictsOptions>(Configuration.GetSection("VideoRestricts"));

            services.AddDbContext<PlayCatDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            ServiceProvider.RegisterService(services);
        }

        private void ServeFromDirectory(IApplicationBuilder app, IHostingEnvironment env, string path)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, path)
                ),
                RequestPath = "/" + path
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            ServeFromDirectory(app, env, "node_modules");
            ServeFromDirectory(app, env, "app");
            ServeFromDirectory(app, env, "Audio");

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404 &&
                   !Path.HasExtension(context.Request.Path.Value) &&
                   !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
