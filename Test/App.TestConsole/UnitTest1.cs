namespace App.TestConsole
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string apppath = @"D:\WORK12\democleanarch\Presentations\App.Console\bin\Debug\net7.0";
            string logpath = @"C:\TestLogs";

            string json = Path.Combine(apppath, "input.json");
            if (File.Exists(json)) File.Delete(json);
            foreach (var log in Directory.GetFiles(logpath))
            {
                File.Delete(log);
            }

            string testpath = Path.Combine(Directory.GetCurrentDirectory(), "input.json");

            File.Copy(json, testpath);

            foreach (var log in Directory.GetFiles(logpath))
            {
                
            }
        }
    }
}

