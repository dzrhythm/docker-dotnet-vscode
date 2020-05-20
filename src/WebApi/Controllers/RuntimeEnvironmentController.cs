using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLib;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuntimeEnvironmentController : ControllerBase
    {
        private readonly ILogger<RuntimeEnvironmentController> _logger;

        public RuntimeEnvironmentController(ILogger<RuntimeEnvironmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public RuntimeEnvironmentSettings Get()
        {
            return RuntimeEnvironmentProvider.GetRuntimeEnvironment();
        }
    }
}