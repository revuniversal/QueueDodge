using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using QueueDodge.Data;
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
            //.UseDefaultHostingConfiguration(args)
            .UseStartup<Startup>()
            .Build();
            
            host.Run();
        }
    }
}
