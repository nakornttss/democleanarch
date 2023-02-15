using App.AppCore.Interfaces;
using App.AppCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestNumberProcessController : ControllerBase
    {
        private readonly IProcessInputOutout _processInputOutout;
        public TestNumberProcessController(IProcessInputOutout processInputOutout)
        {
            _processInputOutout = processInputOutout;
        }

        [HttpPost(Name = "CheckNumberTestResult")]
        public async Task<bool> CheckNumberTestResult([FromBody]InputOutput inputOutput)
        {
            return await _processInputOutout.CheckIsValid(inputOutput);
        }
    }
}