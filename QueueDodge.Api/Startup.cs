using System;
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
using QueueDodge.Api.Websockets;

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

            var us2v2 = new Tuple<string, string>("us", "2v2");
            var us3v3 = new Tuple<string, string>("us", "2v2");
            var us5v5 = new Tuple<string, string>("us", "2v2");
            var usRbg = new Tuple<string, string>("us", "2v2");

            var eu2v2 = new Tuple<string, string>("us", "2v2");
            var eu3v3 = new Tuple<string, string>("us", "2v2");
            var eu5v5 = new Tuple<string, string>("us", "2v2");
            var euRbg = new Tuple<string, string>("us", "2v2");

            WebSocketMultiton.Instances.Add(us2v2, new WebSocketServer());
            WebSocketMultiton.Instances.Add(us3v3, new WebSocketServer());
            WebSocketMultiton.Instances.Add(us5v5, new WebSocketServer());
            WebSocketMultiton.Instances.Add(usRbg, new WebSocketServer());

            WebSocketMultiton.Instances.Add(eu2v2, new WebSocketServer());
            WebSocketMultiton.Instances.Add(eu3v3, new WebSocketServer());
            WebSocketMultiton.Instances.Add(eu5v5, new WebSocketServer());
            WebSocketMultiton.Instances.Add(euRbg, new WebSocketServer());

            app.Map("/ws/us/2v2", builder => WebSocketMultiton.Connect(builder, us2v2));
            app.Map("/ws/us/3v3", builder => WebSocketMultiton.Connect(builder, us3v3));
            app.Map("/ws/us/5v5", builder => WebSocketMultiton.Connect(builder, us5v5));
            app.Map("/ws/us/RBG", builder => WebSocketMultiton.Connect(builder, usRbg));

            app.Map("/ws/eu/2v2", builder => WebSocketMultiton.Connect(builder, eu2v2));
            app.Map("/ws/eu/3v3", builder => WebSocketMultiton.Connect(builder, eu3v3));
            app.Map("/ws/eu/5v5", builder => WebSocketMultiton.Connect(builder, eu5v5));
            app.Map("/ws/eu/RBG", builder => WebSocketMultiton.Connect(builder, euRbg));

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
