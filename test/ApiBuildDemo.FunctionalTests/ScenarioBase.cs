using System.IO;
using ApiBuildDemo.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace ApiBuildDemo.FunctionalTests {
    public class ScenarioBase {

        static string BASELOGINURI = "api/v1/login";
        static string BASEVALUESURI = "api/v1/values";

        public TestServer CreateServer () {
            var hostBuilder = new WebHostBuilder ()
                .UseContentRoot (Directory.GetCurrentDirectory ())
                .ConfigureAppConfiguration (cb => {
                    cb.AddJsonFile ("appsettings.json", optional : false)
                        .AddEnvironmentVariables ();
                })
                .UseEnvironment ("Testing")
                .UseStartup<Startup> ();

            var testServer = new TestServer (hostBuilder);

            return testServer;
        }

        public static class EndPoints {
            public static string SignUp = $"{BASELOGINURI}/signIn";
            public static string AddUser = $"{BASELOGINURI}/addUser";
            public static string ValuesTest = $"{BASEVALUESURI}/test";
            public static string Values = $"{BASEVALUESURI}";
        }
    }

}