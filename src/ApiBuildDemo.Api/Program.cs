using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ApiBuildDemo.Api {
    public class Program {
        public static int Main (string[] args) {

            var configuration = GetConfiguration ();

            Log.Logger = CreateSerilogLogger (configuration);

            try {
                Log.Information ("Configuring web host ");
                var host = BuildWebHost (configuration, args);
                Log.Information ("Starting web host ...");
                host.Run ();

                return 0;
            } catch (Exception ex) {
                Log.Fatal (ex, "Program terminated unexpectedly ");
                return 1;
            } finally {
                Log.CloseAndFlush ();
            }

        }
        private static IWebHost BuildWebHost (IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ()
            .UseContentRoot (Directory.GetCurrentDirectory ())
            .UseConfiguration (configuration)
            .UseSerilog ()
            .Build ();

        private static Serilog.ILogger CreateSerilogLogger (IConfiguration configuration) {
            var seqServerUrl = configuration["Serilog:SeqServerUrl"];
            return new LoggerConfiguration ()
                .MinimumLevel.Verbose ()
                .WriteTo.Seq (string.IsNullOrWhiteSpace (seqServerUrl) ? "http://seq" : seqServerUrl)
                .ReadFrom.Configuration (configuration)
                .CreateLogger ();
        }

        private static IConfiguration GetConfiguration () {
            var whb = new WebHostBuilder ();
            var environment = whb.GetSetting ("environment");
            var builder = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile ($"appsettings.{environment}.json", optional : false, reloadOnChange : true)
                .AddEnvironmentVariables ();

            return builder.Build ();
        }
    }
}