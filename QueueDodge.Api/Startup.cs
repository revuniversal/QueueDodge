using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Http;

namespace QueueDodge.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCaching();
            services.AddScoped<QueueDodgeDB, QueueDodgeDB>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseWebSockets();

            app.Map("/ws/us/2v2", us2v2.Connect);
            app.Map("/ws/us/3v3", us3v3.Connect);
            app.Map("/ws/us/5v5", us5v5.Connect);
            app.Map("/ws/us/RBG", usRbg.Connect);

            app.Map("/ws/eu/2v2", eu2v2.Connect);
            app.Map("/ws/eu/3v3", eu3v3.Connect);
            app.Map("/ws/eu/5v5", eu5v5.Connect);
            app.Map("/ws/eu/RBG", euRbg.Connect);

            app.Run(async context =>
            {
                try
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
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync("ok");
                }
                catch (Exception ex)
                {

                    await context.Response.WriteAsync(ex.Message);
                }
            });
        }

        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);


        /*   CoreCLR RC2 Stuff
        public static void Main(string[] args)
        {
            var application = new Microsoft.AspNet.Hosting.WebHostBuilder()
                .UseConfiguration(WebApplicationConfiguration.GetDefault(args))
                .UseStartup<Startup>()
                .Build();

            application.Run();
        }*/
    }
}
