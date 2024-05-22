using System.Diagnostics;
using HLTVScrapperAPI.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HLTVScrapperAPI.Services
{
    public class TeamScraper : Scraper
    {
       public TeamScraper() : base() {}

       public NestedDictionary Scrape (ScrapeRequest request) 
       {
            NestedDictionary teamStats = new NestedDictionary();
            try
            {
                driver.Navigate().GoToUrl($"https://www.hltv.org/stats");
            
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            
                IWebElement cybotCookieDialogDeclineElement = wait.Until(d =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    return d.FindElement(By.Id("CybotCookiebotDialogBodyButtonDecline"));
                });
            
                cybotCookieDialogDeclineElement.Click();
                Thread.Sleep(TimeSpan.FromSeconds(2));
            
            NestedDictionary playerStatsDict = new NestedDictionary();
            
            IWebElement searchInput = driver.FindElement(By.CssSelector
            (
                "input[class='search-input navsearchinput tt-input']"
            ));
            
            searchInput.Click();
            searchInput.SendKeys("cloud9");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            IWebElement teamItem = driver.FindElement
            (
                By.XPath("/html/body/div[2]/div[8]/div[2]/div[2]/div[1]/div[2]/div[2]/div/div/div/span/div/div/div/div[2]/div[1]/a[1]")
            );
            teamItem.Click();
            IWebElement currentContext = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[10]/a"));
            currentContext.Click();
            string mapsPlayed = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[6]/div[1]/div[1]")).Text;
            string winsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[6]/div[2]/div[1]")).Text;
            string totalKills = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[6]/div[3]/div[1]")).Text;
            string totalDeaths = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[8]/div[1]/div[1]")).Text;
            string roundsPlayed = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[8]/div[2]/div[1]")).Text;
            string killDeathRatio = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[8]/div[3]/div[1]")).Text;
            // Debug.WriteLine($"{mapsPlayed}, {winsDrawsLosses}, {totalKills}, {totalDeaths}, {roundsPlayed}, {killDeathRatio}");
            
            
            }
            catch
            {
                throw;
            }
            return teamStats;
       }
    }
}
