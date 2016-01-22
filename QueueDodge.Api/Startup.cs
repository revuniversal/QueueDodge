﻿using System;
using static System.Console;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using System.Collections.Generic;

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

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                //    builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build().ReloadOnChanged("appsettings.json");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            // services.AddApplicationInsightsTelemetry(Configuration);
            // var connection = @"Server=(localdb)\mssqllocaldb;Database=QueueDodge;Trusted_Connection=True;";

            //services.AddEntityFramework()
            //    .AddSqlServer()
            //    .AddDbContext<QueueDodge.QueueDodgeDB>(options => options.UseSqlServer(connection));

            services.AddMvc();
            services.AddCaching();

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


            app.Map("/ws/us/2v2", US2v2.WebSocketConnect);
            app.Map("/ws/us/3v3", US3v3.WebSocketConnect);


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
