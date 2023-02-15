using App.AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.AppCore.Interfaces
{
    public interface IStorage
    {
        public Task KeepInputOutput(InputOutput inputOutput, bool isValid);
    }
}
