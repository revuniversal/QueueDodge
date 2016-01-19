using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Http;

namespace QueueDodge.Site
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            builder.AddEnvironmentVariables();
            Configuration = builder.Build().ReloadOnChanged("appsettings.json");
        }

        public void ConfigureServices(IServiceCollection services)
        {}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.Run(async context =>
            {
                Console.WriteLine("{0} {1}{2}{3}",
                    context.Request.Method,
                    context.Request.PathBase,
                    context.Request.Path,
                    context.Request.QueryString);
                Console.WriteLine($"Method: {context.Request.Method}");
                Console.WriteLine($"PathBase: {context.Request.PathBase}");
                Console.WriteLine($"Path: {context.Request.Path}");
                Console.WriteLine($"QueryString: {context.Request.QueryString}");

                var connectionFeature = context.Connection;
                Console.WriteLine($"Peer: {connectionFeature.RemoteIpAddress?.ToString()} {connectionFeature.RemotePort}");
                Console.WriteLine($"Sock: {connectionFeature.LocalIpAddress?.ToString()} {connectionFeature.LocalPort}");
                Console.WriteLine($"IsLocal: {connectionFeature.IsLocal}");

                context.Response.ContentLength = 0;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("");
            });
        }

        public static void Main(string[] args)
        {
            var application = new WebApplicationBuilder()
                 .UseConfiguration(WebApplicationConfiguration.GetDefault(args))
                 .UseStartup<Startup>()
                 .Build();

            application.Run();
        }
    }
}
