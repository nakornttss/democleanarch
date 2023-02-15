using App.AppCore.Interfaces;
using App.AppCore.Models;
using App.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Database
{
    public class KeepLogInDb : IStorage
    {
        public async Task KeepInputOutput(InputOutput inputOutput, bool isValid)
        {
            var db = new AppContext();
            var log = new ResultLog();
            log.Id = Guid.NewGuid();
            log.Input1 = inputOutput.Input1;
            log.Input2 = inputOutput.Input2;
            log.Output = inputOutput.Output;
            log.Result = isValid;
            log.ExecutionTime = DateTime.Now;
            db.Add(log);
            await db.SaveChangesAsync();
        }
    }
}
