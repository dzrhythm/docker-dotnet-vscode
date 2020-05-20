using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLib;
using WebUI.Config;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuntimeEnvironmentController : ControllerBase
    {
        private readonly ILogger<RuntimeEnvironmentController> _logger;
        private readonly ApiConfig _config;

        public RuntimeEnvironmentController(ApiConfig config, ILogger<RuntimeEnvironmentController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ServiceRuntimeEnvironment>> Get()
        {
            var runtimes = new List<ServiceRuntimeEnvironment>();
            runtimes.Add(new ServiceRuntimeEnvironment
            {
                Service = "UI",
                Runtime = RuntimeEnvironmentProvider.GetRuntimeEnvironment()
            });

            runtimes.Add(new ServiceRuntimeEnvironment
            {
                Service = "API",
                Runtime = await GetApiRuntime()
            });

            return runtimes;
        }

        private async Task<RuntimeEnvironmentSettings> GetApiRuntime()
        {
            try
            {
                var url = _config.ApiUri + "runtimeenvironment";
                var client = new HttpClient();
                var response = await client.GetStringAsync(url);
                return JsonSerializer.Deserialize<RuntimeEnvironmentSettings>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
            }
            catch (Exception ex)
            {
                return new RuntimeEnvironmentSettings()
                {
                    OSDescription = $"API call failed: {ex}"
                };
            }
        }
    }
}