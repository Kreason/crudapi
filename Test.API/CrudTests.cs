using Assessment.DTO.Response;
using Newtonsoft.Json;
using RestSharp;

namespace Test.API
{
    public class CrudTests
    {
        public static string BaseUrl = "https://testapii.btbracing.online/api/";
        public static string Username = "Kreason";
        public static string Password = "Easy1234";

        // a simple test created to test the authenticate endpoint of the api
        [Fact]
        public static async void Authenticate()
        {
            var client = new RestClient(BaseUrl + $"User/Authenticate?username={Username}&password={Password}");
            var request = new RestRequest();
            request.Method = Method.Post;
            var response = await client.ExecuteAsync(request);
            var data = JsonConvert.DeserializeObject<AuthenticationResponse>(response.Content);
            if (data.Code == 0)
            {
                Assert.Fail(data.Message);
            }
            else
            {
                Assert.True(response.IsSuccessful);
                Assert.True(data.Token != null);
            }   
        }
    }
}