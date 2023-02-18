using System.Diagnostics;
using Newtonsoft.Json;

namespace App.TestConsole
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1, 2, 3, true)]
        [InlineData(1, 2, -9, false)]
        [InlineData(99, 2, 100, false)]
        public async void Test1(int input1, int input2, int output, bool correctResult)
        {
            string apppath = @"D:\WORK12\democleanarch\Presentations\App.Console\bin\Debug\net7.0";
            string json = Path.Combine(apppath, "input.json");
            if (File.Exists(json)) File.Delete(json);

            var test = new InputOutputForTest();
            test.Input1 = input1;
            test.Input2 = input2;
            test.Output = output;

            var testList = new List<InputOutputForTest>();
            testList.Add(test);

            var testContent = JsonConvert.SerializeObject(testList, Formatting.Indented);
            await File.WriteAllTextAsync(json, testContent);

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "App.Console.dll",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WorkingDirectory = apppath
                }
            };

            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string? line = proc.StandardOutput.ReadLine();
                var result = Convert.ToBoolean(line);
                Assert.True(correctResult == result);
            }
        }
    }
}

