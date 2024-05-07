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

        [HttpGet("/api/players")]
        public IEnumerable<Player> GetPlayer()
        {
            return Enumerable.Range(1, 5).Select(index => new Player
            {
                Name = $"{index}",
            })
            .ToArray();
        }

        [HttpGet("/api/test")]
        public void GetClickableElement()
        {
            string hltvUrl = "https://www.hltv.org";
            const string undetectedChromeDriverPath = @"C:/Users/Timothy/source/repos/HLTVScrapperAPI/bin/ChromeDriver/chromedriver.exe";
          
            using (var driver = UndetectedChromeDriver.Create(
            driverExecutablePath: undetectedChromeDriverPath))
            {
                driver.GoToUrl("https://www.hltv.org");
                Thread.Sleep(10000);
            }
        }
    }
}
