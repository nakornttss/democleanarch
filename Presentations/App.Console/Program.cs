using App.AppCore.Applications;
using App.AppCore.Interfaces;
using App.AppCore.Models;
using App.File;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using App.Console;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

IStorage storage = new App.NoStorage.DoNotKeep(); // Default storage
IProcessInputOutout process = new ProcessMock(storage); // Default process

// Configure options
var executionConfig = new ExecutionOptions();
config.GetSection(ExecutionOptions.TagName).Bind(executionConfig);

// Select storage base on configuration
if (executionConfig.Storage == AppStorage.NoStorage)
{
    storage = new App.NoStorage.DoNotKeep();
}
else if (executionConfig.Storage == AppStorage.File)
{
    storage = new App.File.KeepLogInFile();
}
else if (executionConfig.Storage == AppStorage.Database)
{
    storage = new App.Database.KeepLogInDb();
}

// Select process base on configuration
if (executionConfig.AppCore == AppCore.ProcessMock)
{
    process = new ProcessMock(storage);
}
else if (executionConfig.AppCore == AppCore.ProcessMinus)
{
    process = new ProcessMinus(storage);
}
else if (executionConfig.AppCore == AppCore.ProcessPlus)
{
    process = new ProcessPlus(storage);
}

var response = new ResponseMessage();

try
{
    var test = new InputOutput()
    {
        Input1 = Convert.ToInt16(args[0]),
        Input2 = Convert.ToInt16(args[1]),
        Output = Convert.ToInt16(args[2])
    };

    if (test.ValidateInput)
    {
        response.Status = 200;
        response.Message = null;
        response.Result = await process.CheckIsValid(test);
    }
    else
    {
        response.Status = 400;
        response.Message = test.ValidateExplaination;
        response.Result = null;
    }    
}
catch(Exception ex)
{
    response.Status = 500;
    response.Message = ex.Message;
    response.Result = null;
}

var responseText = JsonConvert.SerializeObject(response, Formatting.Indented);
Console.WriteLine(responseText);


