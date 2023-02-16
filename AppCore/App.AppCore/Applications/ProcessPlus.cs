using App.AppCore.Interfaces;
using App.AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.AppCore.Applications
{
    public class ProcessPlus : IProcessInputOutout
    {
        private readonly IStorage? _storage;
        public ProcessPlus(IStorage? storage)
        {
            _storage = storage;
        }
        public async Task<bool> CheckIsValid(InputOutput inputOutput)
        {
            var result = inputOutput.Input1 + inputOutput.Input2 == inputOutput.Output;
            if(_storage != null) await _storage.KeepInputOutput(inputOutput, result);
            return result;
        }
    }
}
