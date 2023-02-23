using App.AppCore.Interfaces;
using App.AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestNumberProcessController : ControllerBase
    {
        private readonly IProcessInputOutout _processInputOutout;
        private readonly IConfiguration _configuration;
        private readonly ExecutionOptions _options;
        public TestNumberProcessController(
            IProcessInputOutout processInputOutout, 
            IConfiguration configuration,
            IOptions<ExecutionOptions> options
            )
        {
            _processInputOutout = processInputOutout;
            _configuration = configuration;
            _options = options.Value;
        }

        [HttpPost(Name = "CheckNumberTestResult")]
        public async Task<IActionResult> CheckNumberTestResult([FromBody]InputOutput inputOutput)
        {
            if (!inputOutput.ValidateInput) return BadRequest();

            try
            {
                return Ok(await _processInputOutout.CheckIsValid(inputOutput));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [HttpGet("GetConfiguration")]
        public IActionResult GetConfiguration()
        {
            return Ok(_options.AppCore.ToString());
        }
    }
}