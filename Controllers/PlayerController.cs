using HLTVScrapperAPI.Models;
using HLTVScrapperAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            PlayerScraper playerScraper = new PlayerScraper();
            var playerDict = playerScraper.Scrape(request: request);
            string prettyJson = JsonConvert.SerializeObject(playerDict, Formatting.Indented);
            var player = PlayerMapper.MapJsonToPlayer(prettyJson);
            var json = JsonConvert.SerializeObject(player);
            return Ok(json);
        }
    }
}
