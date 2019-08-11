using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ApiBuildDemo.Api {
    public class Program {
        public static void Main (string[] args) {
            CreateWebHostBuilder (args).Build ().Run ();
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) {

            var webHost = new WebHostBuilder ()
                .UseKestrel ()
                .UseContentRoot (Directory.GetCurrentDirectory ())
                .ConfigureAppConfiguration ((hostingContext, config) => {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true)
                        .AddJsonFile ($"appsettings.{env.EnvironmentName}.json",
                            optional : true, reloadOnChange : true);
                    config.AddEnvironmentVariables ();
                })
                .ConfigureLogging ((hostingContext, logging) => {
                     Log.Logger = new LoggerConfiguration ()
                        .ReadFrom.Configuration (hostingContext.Configuration)
                        .CreateLogger ();
                    Log.Information ("Starting web host");
                })
                .UseSerilog ()
                .UseStartup<Startup> ();

            return webHost;
        }
    }
}