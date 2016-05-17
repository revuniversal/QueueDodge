using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

    public Startup(IHostingEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddUserSecrets();

        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<QueueDodgeDB>(options => options.UseNpgsql(Configuration["connection"]));

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