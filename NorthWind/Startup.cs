using System.Globalization;
using System.IO;
using DAL.Context;
using DAL.Mapper;
using DAL.Mapper.Interfaces;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthWind.Extensions;
using NorthWind.Middelware;
using NorthWind.Services;
using Services;
using Services.Interfaces;
using UI.Services;
using UI.Services.Interfaces;

namespace NorthWind
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>();
            //services.AddDbContext<NorthWindDbContext>(
            //    options => options.UseSqlServer(_configuration.GetConnectionString("NorthWind")));
            //services.AddScoped<ICategoryData, SqlCategoriesData>();

            services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(_configuration.GetConnectionString("NorthWind")), ServiceLifetime.Singleton);

            services.AddScoped<INorthwindRepository, NorthwindRepository>();
            services.AddScoped<IBLService, BLService>();
            services.AddSingleton<IEntityBuilder, EntityBuilder>();
            services.AddSingleton<IConfigurationService, ConfigurationService>();

            services.AddMvc();
            services.AddMemoryCache();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env,
                              IGreeter greeter,
                              ILoggerFactory loggerFactory,
                              IMemoryCache memoryCache)
        {
            //app.Use(async (context, next) =>
            //{
            //    var isImage = context.Request.Path.ToString().Contains("images");
            //    if (isImage)
            //    {
            //        if (!memoryCache.TryGetValue("cache", out var cached))
            //        {
            //            await next.Invoke();

            //            var response = context.Response;
            //            int bytesBuffer = 1024;
            //            byte[] buffer = new byte[bytesBuffer];

            //            using (MemoryStream ms = new MemoryStream())
            //            {
            //                int readBytes;
            //                while ((readBytes = response.Body.Read(buffer, 0, buffer.Length)) > 0)
            //                {
            //                    ms.Write(buffer, 0, readBytes);
            //                }
                            
            //                var bytes = ms.ToArray();
            //                memoryCache.Set("cache", bytes);
            //            }
 
            //        }
            //        else
            //        {
            //            var bytes = cached;
            //            await context.Response.Body.WriteAsync((byte[])bytes);
            //        }
            //    }
            //    else
            //    {
            //        await next.Invoke();
            //    }
              
            //});

            app.UseMiddleware<ImageCacheMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            else
            {
                app.UseExceptionHandler($"/Error/Error");
                app.UseHsts();
            }

            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");
            logger.LogInformation("Start application. Folder: {0}", Directory.GetCurrentDirectory());

            logger.Log(LogLevel.Information, env.EnvironmentName);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);

            app.UseCookiePolicy();

            app.UseMvc(ConfigureRoutes);

            //app.Run(async (context) =>
            //{
            //    var greeting = greeter.GetMessageOfTheDay();
            //    context.Response.ContentType = "text/plain";
            //    await context.Response.WriteAsync($"Not found");
            //});
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
             routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");
            //images/{image_id}
            routeBuilder.MapRoute(
                name: "Image",
                template: "images/{categoryId}",
                defaults: new { controller = "Categories", action = "RenderImage" });
        }
    }
}
