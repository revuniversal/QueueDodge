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
    public IConfigurationRoot Configuration { get; set; }

    public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(appEnv.ApplicationBasePath)
            .AddEnvironmentVariables()
            .AddUserSecrets();

        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEntityFramework()
            .AddNpgsql()
            .AddDbContext<QueueDodgeDB>(options => options.UseNpgsql(Configuration["connection"]));

        services.AddOptions();

        services.Configure<QueueDodgeOptions>(options =>
        {
            options.apiKey = Configuration["apiKey"];
            options.connection = Configuration["connection"];
        });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    { }
}