using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace App.TestApi
{
    public class UnitTestApi
    {
        [Theory]
        [InlineData("1", "2", "3", 200)]
        [InlineData("11", "-3", "0", 400)]
        [InlineData("abcd", "99", "ddd", 400)]
        public async void TestApi(string input1, string input2, string output, int expectedStatus)
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Create a new HttpClient object
                using var httpClient = new HttpClient();

                // Create the request content with the data to send
                var data = new { input1 = input1, input2 = input2, output = output };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                // Send the request
                var response = await httpClient.PostAsync(config["ApiPath"], content);

                // Check the response status code
                Assert.True(((int)response.StatusCode) == expectedStatus);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}