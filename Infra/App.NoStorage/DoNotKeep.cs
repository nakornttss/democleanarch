using App.AppCore.Interfaces;
using App.AppCore.Models;

namespace App.NoStorage
{
    public class DoNotKeep : IStorage
    {
        public Task KeepInputOutput(InputOutput inputOutput, bool isValid)
        {
            return Task.CompletedTask;
        }
    }
}