using System.Collections.ObjectModel;
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
            
            driver.Navigate().GoToUrl($"https://www.hltv.org/stats");
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            try
            {
                IWebElement cybotCookieDialogDeclineElement = wait.Until(d =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    return d.FindElement(By.Id("CybotCookiebotDialogBodyButtonDecline"));
                });

                cybotCookieDialogDeclineElement.Click();
                Thread.Sleep(TimeSpan.FromSeconds(2));

            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }

            try
            {
                IWebElement searchInput = driver.FindElement(By.CssSelector("input[class='search-input navsearchinput tt-input']"));
                searchInput.Click();
                searchInput.SendKeys("cloud9");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine($"Error: Search input not found {e.Message}");
            }
            
            try
            {
                IWebElement teamItem = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[2]/div[1]/div[2]/div[2]/div/div/div/span/div/div/div/div[2]/div[1]/a[1]"));
                teamItem.Click();
                IWebElement currentContext = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[10]/a"));
                currentContext.Click();
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine($"Error: Team item not found {e.Message}");
            }

            try
            {
                string mapsPlayed = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[6]/div[1]/div[1]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }

            try
            {
                string winsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[6]/div[2]/div[1]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }
            
            try
            {
                string totalKills = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[6]/div[3]/div[1]")).Text;
            }
            catch (NoSuchElementException e)
            { 
                Debug.WriteLine(e.Message);
            }
            
            try
            {
                string totalDeaths = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[6]/div[3]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }
           
            try
            {
                string roundsPlayed = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[8]/div[2]/div[1]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }

            try
            {
                string killDeathRatio = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[8]/div[3]/div[1]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }
            
            //Maps
            try
            {
               IWebElement mapsTab = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[4]/div/div[1]/a[3]"));
                mapsTab.Click();
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }
            
            // Anubis
            try
            {
                string anubisWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[2]/div[1]/span[2]")).Text;
                string anubisWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[2]/div[2]/span[2]")).Text;
                string anubisTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[2]/div[3]/span[2]")).Text;
                string anubisRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[2]/div[4]/span[2]")).Text;
                string anubisRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[2]/div[5]/span[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine($"Error getting anubis stats { e.Message}");
            }
            
            try
            {
                string anubisBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string anubisBiggestWin = "";
            }

            try
            {
                string anubisBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string anubisBiggestDefeat = "";
            }
            
            // Ancient
            try
            {
                string ancientWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[1]/span[2]")).Text;
                string ancientWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[2]/span[2]")).Text;
                string ancientTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[3]/span[2]")).Text;
                string ancientRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[4]/span[2]")).Text;
                string ancientRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[5]/span[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine($"Error getting ancient stats {e.Message}");
            }
           
            try
            {
                string ancientBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string ancientBiggestWin = "";
            }

            try
            {
                string ancientBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string ancientBiggestDefeat = "";
            }

            // Overpass
            try
            {
                string overpassWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[1]/span[2]")).Text;
                string overpassWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[2]/span[2]")).Text;
                string overpassTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[3]/span[2]")).Text;
                string overpassRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[4]/span[2]")).Text;
                string overpassRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[5]/span[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }

            try
            {
                string overpassBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e) 
            { 
                Debug.WriteLine(e.Message); 
                string overpassBiggestWin = ""; 
            }

            try
            {
                string overpassBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string overpassBiggestDefeat = "";
            }

            // Mirage 
            try
            {
                string mirageWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[1]/span[2]")).Text;
                string mirageWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[2]/span[2]")).Text;
                string mirageTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[3]/span[2]")).Text;
                string mirageRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[4]/span[2]")).Text;
                string mirageRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[5]/span[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }
           
            try
            {
                string mirageBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string mirageBiggestWin = "";
            }

            try
            {
                string mirageBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string mirageBiggestDefeat = "";
            }

            // Inferno
            try
            {
                string infernoWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[1]/span[2]")).Text;
                string infernoWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[2]/span[2]")).Text;
                string infernoTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[3]/span[2]")).Text;
                string infernoRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[4]/span[2]")).Text;
                string infernoRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[5]/span[2]")).Text;
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message);
            }
            
            try
            {
                string infernoBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string infernoBiggestWin = "";
            }

            try
            {
                string infernoBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string infernoBiggestDefeat = "";
            }

            // Nuke
            try
            {
                string nukeWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[1]/span[2]")).Text;
                string nukeWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[2]/span[2]")).Text;
                string nukeTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[3]/span[2]")).Text;
                string nukeRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[4]/span[2]")).Text;
                string nukeRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[5]/span[2]")).Text;
            } 
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }
           
            try
            {
                string nukeBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string nukeBiggestWin = "";
            }

            try
            {
                string nukeBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string nukeBiggestDefeat = "";
            }

            // Vertigo
            try
            {
                string vertigoWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[1]/span[2]")).Text;
                string vertigoWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[2]/span[2]")).Text;
                string vertigoTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[3]/span[2]")).Text;
                string vertigoRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[4]/span[2]")).Text;
                string vertigoRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[5]/span[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }   
            
            try
            {
                string vertigoBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string vertigoBiggestWin = "";
            }

            try
            {
                string vertigoBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                string vertigoBiggestDefeat = "";
            }
            
            //Players
            try
            {
                IWebElement playersTab = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[4]/div/div[1]/a[4]"));
                playersTab.Click();
            } 
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }

            try
            {
                IWebElement playersTableDiv = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/table/tbody"));
                ReadOnlyCollection<IWebElement> playerDivs = playersTableDiv.FindElements(By.TagName("tr"));
                KeyValuePair<string, string>[] players = Array.Empty<KeyValuePair<string, string>>();
                foreach (IWebElement item in playerDivs.ToArray())
                {
                    try
                    {
                        KeyValuePair<string, string> name = KeyValuePair.Create("name", item.FindElement(By.CssSelector("td.playerCol.bold a")).Text);
                        KeyValuePair<string, string> maps = KeyValuePair.Create("maps", item.FindElement(By.CssSelector("td.statsDetail")).Text);
                        KeyValuePair<string, string> rounds = KeyValuePair.Create("rounds", item.FindElement(By.CssSelector("td.statsDetail.gtSmartphone-only")).Text);
                        KeyValuePair<string, string> rating2 = KeyValuePair.Create("rating2", item.FindElement(By.CssSelector(".ratingCol")).Text);
                        Debug.WriteLine($"{name}, {maps}, {rounds}, {rating2}");
                    }
                    catch (NoSuchElementException e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
            }
            
            this.Dispose();
            return teamStats;
       }
    }
}
