using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiBuildDemo.Api.Models;
using ApiBuildDemo.Infrastructure.Models;
using Newtonsoft.Json;
using Xunit;

namespace ApiBuildDemo.FunctionalTests {
    public class ValueScenario : ScenarioBase {

        [Fact]
        public async Task When_CallValuesTest_Expect_ResponseOkStatusCode () {

            using (var server = CreateServer ()) {

                var response = await server.CreateClient ()
                    .GetAsync (EndPoints.ValuesTest);

                response.EnsureSuccessStatusCode ();
            }
        }

        [Fact]
        public async Task When_GetValues_Expect_ResponsOkStatusCode () {
            using (var server = CreateServer ()) {

                var client = server.CreateClient ();

                var content = new StringContent (
                    JsonConvert.SerializeObject (new User {
                        UserName = "test",
                            Password = "test"
                    }), UTF8Encoding.UTF8, "application/json");
                var responseLogin = await server.CreateClient ()
                    .PostAsync (EndPoints.AddUser, content);
                var result = await responseLogin.Content.ReadAsStringAsync ();
                var user = JsonConvert.DeserializeObject<UserDto> (result);

                client.DefaultRequestHeaders.Add ("Authorization", "Bearer " + user.Token);
                var response = await client.GetAsync (EndPoints.Values);

                response.EnsureSuccessStatusCode ();
            }
        }
    }
}