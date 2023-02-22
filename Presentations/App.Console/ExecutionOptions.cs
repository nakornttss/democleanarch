namespace App.Console
{
    public class ExecutionOptions
    {
        public const string TagName = "Execution";
        public AppCore? AppCore { get; set; }
        public AppStorage? Storage { get; set; }
    }
    public enum AppStorage
    {
        NoStorage,
        File,
        Database
    }
    public enum AppCore
    {
        ProcessMock,
        ProcessMinus,
        ProcessPlus
    }
}
