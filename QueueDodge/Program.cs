using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QueueDodge.Site
{
    public class Program
    {
        public IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            var arguments = args.ToList();

            var helpCommandExists = arguments.Where(p => p == "help" || p == "-h" || p == "--help").Any();
            var queryCommandExists = arguments.Where(p => p == "query").Any();

            if (helpCommandExists)
            {
                Help();
            }
            else if (queryCommandExists)
            {
                Task.WaitAll(Query(arguments));
            }
            else
            {
                Help();
            }
        }
        public async static Task Query(IList<string> arguments)
        {
            var regionParamExists = arguments.Where(p => p == "-r" || p == "--region").Any();
            var bracketParamExists = arguments.Where(p => p == "-b" || p == "--bracket").Any();
            var intervalParamExists = arguments.Where(p => p == "-i" || p == "--interval").Any();
            var silenceParamExists = arguments.Where(p => p == "-s" || p == "--silence").Any();

            var requiredParametersExist = regionParamExists && bracketParamExists && intervalParamExists;

            if (requiredParametersExist)
            {
                var regionParam = (arguments.IndexOf("-r") != 0 ? arguments.IndexOf("-r") : arguments.IndexOf("--region")) + 1;
                var bracketParam = (arguments.IndexOf("-b") != 0 ? arguments.IndexOf("-b") : arguments.IndexOf("--bracket")) + 1;
                var intervalParam = (arguments.IndexOf("-i") != 0 ? arguments.IndexOf("-i") : arguments.IndexOf("--interval")) + 1;
                //var hostParam = arguments.IndexOf("-h") != 0 ? arguments.IndexOf("-h") : arguments.IndexOf("--host");

                var regionValueExists = !string.IsNullOrWhiteSpace(arguments[regionParam]);
                var bracketValueExists = !string.IsNullOrWhiteSpace(arguments[bracketParam]);
                var intervalValueExists = !string.IsNullOrWhiteSpace(arguments[intervalParam]);
                //  var hostValueExists = !string.IsNullOrWhiteSpace(arguments[hostParam]);

                var valuesExist = regionValueExists && bracketValueExists && intervalValueExists;

                if (valuesExist)
                {
                    try
                    {
                        var region = arguments[regionParam];
                        var bracket = arguments[bracketParam];
                        var interval = arguments[intervalParam];
                       
                        //  var host = arguments[hostParam];

                        //  if (!hostValueExists)
                        //  {
                        var host = "localhost";
                        // }

                        var uri = $@"http://{host}/api/region/{region}/bracket/{bracket}?locale=en_us";
                        var client = new HttpClient();
                        Console.WriteLine($"Fetching data for the {region} {bracket} bracket...  Ctrl + C to cancel this job.");
                        while (true)
                        {
                            var response = await client.GetAsync(uri);

                            if(!silenceParamExists)
                                Console.WriteLine($"{bracket}-{region}:  " + response.StatusCode + " at " + DateTime.Now.ToLocalTime());

                            await Task.Delay(Convert.ToInt32(interval) * 1000);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error:  " + ex.Message);
                    }
                }
                else
                {
                    Help();
                }

            }
            else
            {
                Help();
            }
        }

        public static void Help()
        {
            Console.WriteLine(@"
Queue Dodge CLI


Usage:  dotnet queuedodge [command] [arguments]

Arguments:
    [command]           The command to execute.
    [arguments]         Arguments to pass to the command.

Commands:
    query               Queries ladder information from the battle.net api.
    help|-h|--help      Shows this help information.

Arguments:
    query
        -r|--region         The region to query.  This should be us or eu.
        -b|--bracket        The bracket to query.  This should be 2v2, 3v3, 5v5, or rbg.    
        -i|--interval       The interval between queries in seconds.
");
        }
    }
}

