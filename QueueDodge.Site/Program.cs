using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace QueueDodge.Site
{
    public class Program
    {
        public IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            //.UseDefaultHostingConfiguration(args)
            .UseStartup<Startup>()
            .Build();

            host.Run();
        }
    }
}
