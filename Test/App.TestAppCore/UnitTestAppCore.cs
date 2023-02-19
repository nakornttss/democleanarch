using App.AppCore.Applications;
using App.AppCore.Interfaces;
using App.AppCore.Models;
using App.NoStorage;

namespace App.TestAppCore
{
    public class UnitTestAppCore
    {
        [Theory]
        [InlineData(1, 2, 3, true)]
        [InlineData(1, 2, -9, false)]
        [InlineData(99, 2, 100, false)]
        public async void TestProcessPlus(int input1, int input2, int output, bool correctResult)
        {
            IStorage storage = new DoNotKeep();
            IProcessInputOutout process = new ProcessPlus(storage);

            var test = new InputOutput();
            test.Input1 = input1;
            test.Input2 = input2;
            test.Output = output;

            var result = await process.CheckIsValid(test);

            Assert.True(correctResult == result);
        }

        [Theory]
        [InlineData(1, 2, -1, true)]
        [InlineData(1, 2, -9, false)]
        [InlineData(99, 2, 100, false)]
        public async void TestProcessMinus(int input1, int input2, int output, bool correctResult)
        {
            IStorage storage = new DoNotKeep();
            IProcessInputOutout process = new ProcessMinus(storage);

            var test = new InputOutput();
            test.Input1 = input1;
            test.Input2 = input2;
            test.Output = output;

            var result = await process.CheckIsValid(test);

            Assert.True(correctResult == result);
        }
    }
}