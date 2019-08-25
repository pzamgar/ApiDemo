using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiBuildDemo.Infrastructure.Models;
using Newtonsoft.Json;
using Xunit;

namespace ApiBuildDemo.FunctionalTests {
    public class LoginScenario : ScenarioBase {
        [Fact]
        public async Task When_AddedUser_Expect_ResponseOkStatusCode () {

            using (var server = CreateServer ()) {

                var content = new StringContent (BuildUser (), UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateClient ()
                    .PostAsync (EndPoints.AddUser, content);

                response.EnsureSuccessStatusCode ();
            }
        }

        private string BuildUser () {
            var user = new User {
                UserName = "test",
                Password = "12345"
            };
            return JsonConvert.SerializeObject (user);
        }
    }
}