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

            string name = request.Name;
            bool teamexists = teamScraper.IsExist(name);
            
            string timeFrame = request.TimeFrame;
            bool validtimeframe = teamScraper.IsTimeFrameValid(timeFrame);
            
            if (!validtimeframe)
            {
                return NotFound();
            }

            if (!teamexists)
            {
                return NotFound();
            }
            
            Team team = teamScraper.Scrape(request);
            return Ok(team);
        }
    }
}