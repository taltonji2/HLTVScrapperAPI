using HLTVScrapperAPI.Models;
using Microsoft.AspNetCore.Mvc;
using SeleniumUndetectedChromeDriver;

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

        //[HttpGet("/api/players")]
        //public IEnumerable<Player> GetPlayer()
        //{
        //}

        [HttpGet("/api/player")]
        public void GetClickableElement()
        {
            Services.PlayerScraper.ScrapePlayerStats();
        }
    }
}
