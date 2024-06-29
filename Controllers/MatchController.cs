using HLTVScrapperAPI.Models;
using HLTVScrapperAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HLTVScrapperAPI.Controllers
{
    [ApiController]
    [Route("/api/scrape/match")]
    public class MatchController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;

        public MatchController(ILogger<PlayerController> logger)
        {
            _logger = logger;
        }

        [HttpGet("all")]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public IActionResult GetAllMatches()
        {
            MatchScraper matchScraper = new MatchScraper();

            ScrapeResult<List<Match>> result = matchScraper.ScrapeAll();
            if (result.Success) return Ok(result.ScrapeObject);
            else return StatusCode(500, result.Errors);
        }

        [HttpGet("toptier")]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public IActionResult GetTopTierMatches()
        {
            MatchScraper matchScraper = new MatchScraper();

            ScrapeResult<List<Match>> result = matchScraper.ScrapeTopTier();
            if (result.Success) return Ok(result.ScrapeObject);
            else return StatusCode(500, result.Errors);
        }

        [HttpGet("lan")]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public IActionResult GetLANMatches()
        {
            MatchScraper matchScraper = new MatchScraper();

            ScrapeResult<List<Match>> result = matchScraper.ScrapeLAN();
            if (result.Success) return Ok(result.ScrapeObject);
            else return StatusCode(500, result.Errors);
        }
    }
}
