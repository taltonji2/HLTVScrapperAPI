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
            public Models.Team team { get; set; }
            public int HttpStatusCode { get; set; }
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
            EntityExistScraper entityExistScrape = new EntityExistScraper();

            string name = request.Name;
            string timeFrame = request.TimeFrame;
            bool teamexists = entityExistScrape.Scrape(name);
            bool validtimeframe = request.IsTimeFrameValid(timeFrame);
            
            if (!validtimeframe || !teamexists) { return NotFound(); }

            Team team = teamScraper.Scrape(request);
            if (team != null) { return Ok(team); }
            else { return StatusCode(500, "Internal server error"); }
        }
    }
}