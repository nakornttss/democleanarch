using App.AppCore.Interfaces;
using App.AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.AppCore.Applications
{
    public class ProcessMock : IProcessInputOutout
    {
        private readonly IStorage _storage;
        public ProcessMock(IStorage storage)
        {
            _storage = storage;
        }
        public async Task<bool> CheckIsValid(InputOutput inputOutput)
        {
            var result = true;
            await _storage.KeepInputOutput(inputOutput, result);
            return result;
        }
    }
}
