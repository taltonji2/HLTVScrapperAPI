using HLTVScrapperAPI.Models;
using HLTVScrapperAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HLTVScrapperAPI.Controllers 
{
    [ApiController]
    [Route("/api/team")]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;

        private struct TeamScrapeResponse
        {
            public Models.Team team { get; set; }
            public int HttpStatusCode { get; set; }
        }

        public TeamController(ILogger<TeamController> logger)
        {
            _logger = logger;
        }

        [HttpGet("scrape")]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public IActionResult GetByNameAndTimeframe([FromQuery] string name)
        {
            TeamScraper teamScraper = new TeamScraper();

            TeamResult result = teamScraper.Scrape(name);
            if (result.Success) return Ok(result.Team);
            else return StatusCode(500, result.Errors);
        }
    }
}