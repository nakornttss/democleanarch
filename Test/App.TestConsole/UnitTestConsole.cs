using System.Diagnostics;
using Newtonsoft.Json;
using App.Console;
using Microsoft.Extensions.Configuration;

namespace App.TestConsole
{
    public class UnitTestConsole
    {
        [Theory]
        [InlineData("1", "2", "3", 200)]
        [InlineData("11", "-3", "0", 400)]
        [InlineData("abcd", "99", "ddd", 500)]
        public void TestConsole(string input1, string input2, string output, int expectedStatus)
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        Arguments = $"App.Console.dll {input1} {input2} {output}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        WorkingDirectory = config["ConsoleAppPath"]
                    }
                };

                proc.Start();
                string allResult = "";
                while (!proc.StandardOutput.EndOfStream)
                {
                    string? line = proc.StandardOutput.ReadLine();
                    if (line != null) allResult += line;
                }

                var result = JsonConvert.DeserializeObject<ResponseMessage>(allResult);
                if (result == null) Assert.Fail("Application error");
                else Assert.True(result.Status == expectedStatus);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}

