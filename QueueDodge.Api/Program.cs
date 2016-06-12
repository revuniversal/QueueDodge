using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace QueueDodge.Api
{
    public class Program
    {
        public IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args) 
        {
            var host = new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseUrls("http://*:5001")
            //.UseDefaultHostingConfiguration(args)
            .UseStartup<Startup>()
            .Build();
            
            host.Run();
        }
    }
}
