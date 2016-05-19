using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace QueueDodge.Site
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
          //  var builder = new ConfigurationBuilder()
              //  .AddJsonFile(@"C:\Users\NickA\Projects\QueueDodge\QueueDodge.Site\appsettings.json");

         //   builder.AddEnvironmentVariables();
          //  Configuration = builder.Build().ReloadOnChanged("~/appsettings.json");
        }

        public void ConfigureServices(IServiceCollection services)
        {}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            
            app.Use(async (context, next) =>
            {

                await next();

                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {

                    context.Request.Path = "/index.html"; // Put your Angular root page here 

                    await next();

                }

            });
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
