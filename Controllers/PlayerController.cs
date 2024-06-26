using HLTVScrapperAPI.Models;
using HLTVScrapperAPI.Models.Player;
using HLTVScrapperAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HLTVScrapperAPI.Controllers
{
    [ApiController]
    [Route("/api/scrape/player")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            _logger = logger;
        }

        [HttpGet("byname")]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public IActionResult GetByNameAndTimeframe([FromQuery] string name)
        {
            PlayerScraper playerScraper = new PlayerScraper();

            ScrapeResult<Player> result = playerScraper.ScrapePlayer(name);
            if (result.Success) return Ok(result.ScrapeObject);
            else return StatusCode(500, result.Errors);
        }
    }
}
