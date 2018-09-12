using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdvancedMiddleware
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Information);
            var logger = loggerFactory.CreateLogger("Midleware demo");

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.Use(async (context, next) =>
            //{
            //    var timer = Stopwatch.StartNew();

            //    logger.LogInformation($"==> beginning request in {env.ApplicationName}");
            //    await next();

            //    logger.LogInformation($" ==> completed request {timer.ElapsedMilliseconds} ms");
            //});
            app.UseEnvironmentMiddleware();

            app.Map("/contacts",
                a => a.Run(async context => { await context.Response.WriteAsync("Here are youre contacts"); }));

            app.MapWhen(context => context.Request.Headers["User-Agent"].First().Contains("Chrome"), ChromeRoute);

            app.UseStaticFiles();

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("Hello world");
            });
        }

        private void ChromeRoute(IApplicationBuilder app)
        {
            app.Run(async context => { await context.Response.WriteAsync("Hello Chrome"); });
        }
    }
}