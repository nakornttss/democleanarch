using Newtonsoft.Json;
using System.Text;

namespace App.TestApi
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1, 2, 3, true)]
        [InlineData(1, 2, -9, false)]
        [InlineData(99, 2, 100, false)]
        public async void Test1(int input1, int input2, int output, bool correctResult)
        {
            // Create a new HttpClient object
            using var httpClient = new HttpClient();

            // Create the request content with the data to send
            var data = new { input1 = input1, input2 = input2, output = output };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            // Send the request
            var response = await httpClient.PostAsync("https://localhost:7046/TestNumberProcess", content);

            // Check the response status code
            Assert.True(response.IsSuccessStatusCode);

            var result = Convert.ToBoolean(await response.Content.ReadAsStringAsync());

            Assert.True(correctResult == result);
        }
    }
}