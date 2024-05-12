using HLTVScrapperAPI.Models;
using HLTVScrapperAPI.Models.ScrapeRequest;
using Microsoft.AspNetCore.Mvc;
using SeleniumUndetectedChromeDriver;
using System.Diagnostics;

namespace HLTVScrapperAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/api/player")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public IActionResult ScrapePlayer([FromBody] PlayerScrapeRequest request)
        {
            var dict = Services.PlayerScraper.ScrapePlayerStats();
            return Ok(dict);
        }
    }
}
