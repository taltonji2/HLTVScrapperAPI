using HLTVScrapperAPI.Models;
using HLTVScrapperAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HLTVScrapperAPI.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase 
    {
        private readonly ILogger<TeamController> _logger;

        private struct TeamScrapeResponse
        {
            public Team team { get; set; }
            public HttpStatusCode
        }

        public TeamController(ILogger<TeamController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/api/team")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult ScrapeTeam([FromBody] TeamScrapeRequest request)
        {
            TeamScraper teamScraper = new TeamScraper();
            var team, var a = teamScraper.Scrape(request: request);
            return Ok(team);
        }
    }
}