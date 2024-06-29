using HLTVScrapperAPI.Models;
using HLTVScrapperAPI.Models.Team;
using HLTVScrapperAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HLTVScrapperAPI.Controllers 
{
    [ApiController]
    [Route("/api/scrape/team")]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;

        private struct TeamScrapeResponse
        {
            public Team team { get; set; }
            public int HttpStatusCode { get; set; }
        }

        public TeamController(ILogger<TeamController> logger)
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
            TeamScraper teamScraper = new TeamScraper();

            ScrapeResult<Team> result = teamScraper.Scrape(name);
            if (result.Success) return Ok(result.ScrapeObject);
            else return StatusCode(500, result.Errors);
        }
    }
}