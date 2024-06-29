using HLTVScrapperAPI.Models;
using HLTVScrapperAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HLTVScrapperAPI.Controllers
{
    [ApiController]
    [Route("/api/scrape/event")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;

        public EventController(ILogger<EventController> logger)
        {
            _logger = logger;
        }

        [HttpGet("bigevent")]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public IActionResult GetByNameAndTimeframe([FromQuery] string name)
        {
            EventScraper eventScraper = new EventScraper();

            ScrapeResult<BigEvent> result = eventScraper.ScrapeBigEvents();
            if (result.Success) return Ok(result.ScrapeObject);
            else return StatusCode(500, result.Errors);
        }
    }
}
