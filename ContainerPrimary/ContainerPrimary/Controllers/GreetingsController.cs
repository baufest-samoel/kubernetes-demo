using ContainerPrimary.Services;
using ContainerPrimary.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContainerPrimary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GreetingsController : Controller
    {
        private readonly MySettings _cfg;
        private readonly Guid _guid;

        public GreetingsController(IOptions<MySettings> cfg,
         GuidProvider guidprovider)
        {
            _cfg = cfg.Value;
            _guid = guidprovider.Id;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var secondaryMessage = await GetSecondaryResponse();
            return Ok(new
            {
                Message = $"Hola soy el servicio primario {_guid} corriendo en {Environment.MachineName}",
                SecondaryMessage = secondaryMessage
            });
        }

        private async Task<string> GetSecondaryResponse()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{_cfg.SecondaryUri}/api/identification");
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }

                    return $"El servicio secundario corriendo en {_cfg.SecondaryUri} no se ha podido identificar y devolvio el codigo {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Error {ex.GetType().Name} ('{ex.Message}') en servicio secundario corriendo en {_cfg.SecondaryUri}";
            }
        }
    }
}