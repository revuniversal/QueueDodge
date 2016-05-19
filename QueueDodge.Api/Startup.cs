using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace QueueDodge.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            //// Set up configuration sources.
            var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables("queueDodgeApi_");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMemoryCache();
            services.AddOptions();
            services.AddScoped<QueueDodgeDB, QueueDodgeDB>();

            services.Configure<QueueDodgeOptions>(options =>
            {
                options.apiKey = Configuration["apiKey"];
                options.connection = Configuration["connection"];
            });

            services.Configure<QueueDodgeApiOptions>(options =>
            {
                options.apiKey = Configuration["apiKey"];
                options.connection = Configuration["connection"];
            });

            services.AddScoped<QueueDodgeSeed, QueueDodgeSeed>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            app.UseWebSockets();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }

            app.Map("/ws/us/2v2", us2v2.Connect);
            app.Map("/ws/us/3v3", us3v3.Connect);
            app.Map("/ws/us/5v5", us5v5.Connect);
            app.Map("/ws/us/RBG", usRbg.Connect);

            app.Map("/ws/eu/2v2", eu2v2.Connect);
            app.Map("/ws/eu/3v3", eu3v3.Connect);
            app.Map("/ws/eu/5v5", eu5v5.Connect);
            app.Map("/ws/eu/RBG", euRbg.Connect);


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
       


        }


    }
}
