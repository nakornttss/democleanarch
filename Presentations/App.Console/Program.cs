using App.AppCore.Applications;
using App.AppCore.Interfaces;
using App.AppCore.Models;
using App.File;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using App.Console;
using static System.Net.Mime.MediaTypeNames;

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

IStorage storage = new App.NoStorage.DoNotKeep(); // Default storage
IProcessInputOutout process = new ProcessMock(storage); // Default process

// Select storage base on configuration
if (config["Execution:Storage"] == "NoStorage")
{
    storage = new App.NoStorage.DoNotKeep();
}
else if (config["Execution:Storage"] == "File")
{
    storage = new App.File.KeepLogInFile();
}
else if (config["Execution:Storage"] == "Database")
{
    storage = new App.Database.KeepLogInDb();
}

// Select process base on configuration
if (config["Execution:AppCore"] == "ProcessMock")
{
    process = new ProcessMock(storage);
}
else if (config["Execution:AppCore"] == "ProcessMinus")
{
    process = new ProcessMinus(storage);
}
else if (config["Execution:AppCore"] == "ProcessPlus")
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

/*
string path = Path.Combine(Directory.GetCurrentDirectory(), "input.json");

var inputs = JsonConvert.DeserializeObject<List<InputOutput>>(await System.IO.File.ReadAllTextAsync(path));

if(inputs != null)
{
    foreach (var test in inputs)
    {
        Console.WriteLine(await process.CheckIsValid(test));        
        Thread.Sleep(1000);
    }
}
*/

