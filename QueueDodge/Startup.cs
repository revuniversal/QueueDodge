using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using QueueDodge;
/// <summary>
/// We need a Startup class in this class library,
/// because the ef command line migration tool cannot work,
/// if the DbContext is in a different project
/// </summary>
public class Startup
{
    public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
    {
        // link the original startup project's root for the config.json
        var builder = new ConfigurationBuilder()
            .SetBasePath(appEnv.ApplicationBasePath)
            .AddJsonFile("config.json");
          //  .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

        builder.AddEnvironmentVariables();
        Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEntityFramework()
            .AddSqlServer()
            .AddDbContext<QueueDodgeDB>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
    }
}