using App.AppCore.Models;
using App.Database;
using Microsoft.EntityFrameworkCore;

namespace App.TestKeepInDatabase
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1, 2, 3, true)]
        [InlineData(1, 2, -9, false)]
        [InlineData(99, 2, 100, false)]
        public async void Test1(int input1, int input2, int output, bool correctResult)
        {
            var db = new Database.AppContext();
            db.RemoveRange(db.ResultLogs);
            await db.SaveChangesAsync();

            var inputOutput = new InputOutput();
            inputOutput.Input1 = input1;
            inputOutput.Input2 = input2;
            inputOutput.Output = output;

            var keepProcess = new KeepLogInDb();
            await keepProcess.KeepInputOutput(inputOutput, correctResult);

            var log = await db.ResultLogs.FirstOrDefaultAsync();
            Assert.NotNull(log);

            Assert.Equal(input1, log.Input1);
            Assert.Equal(input2, log.Input2);
            Assert.Equal(output, log.Output);
            Assert.Equal(correctResult, log.Result);
        }
    }
}