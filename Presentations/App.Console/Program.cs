using App.AppCore.Applications;
using App.AppCore.Interfaces;
using App.AppCore.Models;
using App.File;

IStorage storage = new KeepLogInFile();
IProcessInputOutout process = new ProcessPlus(storage);

var test = new InputOutput();
test.Input1 = 1;
test.Input2 = 2;
test.Output = 3;

await process.CheckIsValid(test);
