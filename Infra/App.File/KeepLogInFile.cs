using App.AppCore.Interfaces;
using App.AppCore.Models;
using App.File.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace App.File
{
    public class KeepLogInFile : IStorage
    {
        public async Task KeepInputOutput(InputOutput inputOutput, bool isValid)
        {
            var log = new FileSchema();
            log.Input1 = inputOutput.Input1;
            log.Input2 = inputOutput.Input2;
            log.Output = inputOutput.Output;
            log.Result = isValid;
            log.ExecutionTime = DateTime.Now;
            string json = JsonConvert.SerializeObject(log);
            string path = @"C:\TestLogs\";
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);
            await System.IO.File.WriteAllTextAsync(@"C:\TestLogs\" + log.ExecutionTime.ToString("ddMMyyyyHHmmss") + ".json", json);
        }
    }
}
