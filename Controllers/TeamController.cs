using HLTVScrapperAPI.Models;
using HLTVScrapperAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HLTVScrapperAPI.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase 
    {
        private readonly ILogger<TeamController> _logger;

        public TeamController(ILogger<TeamController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/api/team")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public IActionResult ScrapeTeam([FromBody] TeamScrapeRequest request)
        {
            TeamScraper teamScraper = new TeamScraper();
            var teamDict = teamScraper.Scrape(request: request);
            string prettyJson = JsonConvert.SerializeObject(teamDict, Formatting.Indented);
            // var team = TeamMapper.MapJsonToTeam(prettyJson);
            // var json = JsonConvert.SerializeObject(team);
            return Ok();
        }
    }
}