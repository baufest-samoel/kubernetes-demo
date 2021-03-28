using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Secondary.Services;

namespace Secondary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentificationController : Controller
    {
        private readonly Guid _id;

        public IdentificationController(GuidProvider provider)
        {
            _id = provider.Id;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok($"Soy el secundario con nombre {_id} hosteado en {Environment.MachineName}");
        }
    }
}
