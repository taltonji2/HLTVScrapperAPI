using OpenQA.Selenium;
using SeleniumUndetectedChromeDriver;
using System.Diagnostics;

namespace HLTVScrapperAPI.Services
{
    public class PlayerScraper : Scraper
    {
        private static void ScrapePlayerStatsAction(IWebDriver driver)
        {
            IWebElement searchInput = driver.FindElement(By.XPath(
                "/html/body/div[2]/div[8]/div[2]/div[2]/div/div[2]/div[2]/div/div/div/span/input"
            ));
            
            searchInput.Click();
            
            searchInput.SendKeys("s1mple");
            
            Thread.Sleep(TimeSpan.FromSeconds(2));
            
            IWebElement playerItem = driver.FindElement(By.XPath(
                "/html/body/div[2]/div[8]/div[2]/div[2]/div/div[2]/div[2]/div/div/div/span/div/div/div/div[2]/div[1]/a[1]"
            ));
            
            playerItem.Click();
        }

        public static void ScrapePlayerStats()
        {
            Scrape(route: "stats", action: ScrapePlayerStatsAction);
        }
    }
}
