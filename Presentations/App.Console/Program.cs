using App.AppCore.Applications;
using App.AppCore.Interfaces;
using App.AppCore.Models;
using App.File;
using Newtonsoft.Json;

IStorage storage = new KeepLogInFile();
IProcessInputOutout process = new ProcessPlus(storage);

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
 

