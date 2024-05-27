using HLTVScrapperAPI.Models;
using HLTVScrapperAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            PlayerScraper playerScraper = new PlayerScraper();
            Player player = playerScraper.Scrape(request: request);
            var json = JsonConvert.SerializeObject(player);
            return Ok(json);
        }
    }
}
