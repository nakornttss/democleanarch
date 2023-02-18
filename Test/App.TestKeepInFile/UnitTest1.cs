using App.AppCore.Models;
using App.File;
using App.File.Models;
using Newtonsoft.Json;

namespace App.TestKeepInFile
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1, 2, 3, true)]
        [InlineData(1, 2, -9, false)]
        [InlineData(99, 2, 100, false)]
        public async void Test1(int input1, int input2, int output, bool correctResult)
        {
            var logpath = @"C:\TestLogs";
            if(Directory.Exists(logpath)) Directory.Delete(logpath, true);
            Directory.CreateDirectory(logpath);

            var inputOutput = new InputOutput();
            inputOutput.Input1 = input1;
            inputOutput.Input2 = input2;
            inputOutput.Output = output;

            var log = new FileSchema();
            log.Input1 = inputOutput.Input1;
            log.Input2 = inputOutput.Input2;
            log.Output = inputOutput.Output;
            log.Result = correctResult;
            log.ExecutionTime = DateTime.Now;

            var keepProcess = new KeepLogInFile();
            await keepProcess.KeepInputOutput(inputOutput, log.Result);

            foreach(var file in Directory.GetFiles(logpath))
            {
                var text = await System.IO.File.ReadAllTextAsync(file);
                var result = JsonConvert.DeserializeObject<FileSchema>(text);

                if(result != null )
                {
                    Assert.True(result.Input1 == log.Input1);
                    Assert.True(result.Input2 == log.Input2);
                    Assert.True(result.Output == log.Output);
                    Assert.True(result.Result == log.Result);
                }
                else
                {
                    Assert.True(false);
                }                
            }
        }
    }
}

