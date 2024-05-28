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

        public Team Scrape(ScrapeRequest request)
        {
            try 
            { 
                Team team = new Team();
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
                    team.MapsPlayed = mapsPlayed;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string winsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[6]/div[2]/div[1]")).Text;
                    //calculate win rate
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string roundsPlayed = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[8]/div[2]/div[1]")).Text;
                    team.RoundsPlayed = roundsPlayed;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string killDeathRatio = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[8]/div[3]/div[1]")).Text;
                    team.KD = killDeathRatio;
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
                    team.Anubis.WinRate = anubisWinsRate;
                    team.Anubis.RoundsPlayed = anubisTotalRounds;
                    team.Anubis.RoundWinRateAfterFirstKill = anubisRoundWinPercentAfterFirstKill;
                    team.Anubis.RoundWinRateAfterFirstDeath = anubisRoundWinPercentAfterFirstDeath;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error getting anubis stats {e.Message}");
                }

                try
                {
                    string anubisBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Anubis.BiggestWin = anubisBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string anubisBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Anubis.BiggestLoss = anubisBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                // Ancient
                try
                {
                    string ancientWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[1]/span[2]")).Text;
                    string ancientWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[2]/span[2]")).Text;
                    string ancientTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[3]/span[2]")).Text;
                    string ancientRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[4]/span[2]")).Text;
                    string ancientRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[5]/span[2]")).Text;
                    team.Ancient.WinRate = ancientWinsRate;
                    team.Ancient.RoundsPlayed = ancientTotalRounds;
                    team.Ancient.RoundWinRateAfterFirstKill = ancientRoundWinPercentAfterFirstKill;
                    team.Ancient.RoundWinRateAfterFirstDeath = ancientRoundWinPercentAfterFirstDeath;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error getting ancient stats {e.Message}");
                }

                try
                {
                    string ancientBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Ancient.BiggestWin = ancientBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string ancientBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Ancient.BiggestLoss = ancientBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                // Overpass
                try
                {
                    string overpassWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[1]/span[2]")).Text;
                    string overpassWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[2]/span[2]")).Text;
                    string overpassTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[3]/span[2]")).Text;
                    string overpassRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[4]/span[2]")).Text;
                    string overpassRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[5]/span[2]")).Text;
                    team.Overpass.WinRate = overpassWinsRate;
                    team.Overpass.RoundsPlayed = overpassTotalRounds;
                    team.Overpass.RoundWinRateAfterFirstKill = overpassRoundWinPercentAfterFirstKill;
                    team.Overpass.RoundWinRateAfterFirstDeath = overpassRoundWinPercentAfterFirstDeath;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string overpassBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Overpass.BiggestWin = overpassBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string overpassBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Overpass.BiggestLoss = overpassBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                // Mirage 
                try
                {
                    string mirageWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[1]/span[2]")).Text;
                    string mirageWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[2]/span[2]")).Text;
                    string mirageTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[3]/span[2]")).Text;
                    string mirageRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[4]/span[2]")).Text;
                    string mirageRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[5]/span[2]")).Text;
                    team.Mirage.WinRate = mirageWinsRate;
                    team.Mirage.RoundsPlayed = mirageTotalRounds;
                    team.Mirage.RoundWinRateAfterFirstKill = mirageRoundWinPercentAfterFirstKill;
                    team.Mirage.RoundWinRateAfterFirstDeath = mirageRoundWinPercentAfterFirstDeath;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string mirageBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Mirage.BiggestWin = mirageBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string mirageBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Mirage.BiggestLoss = mirageBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                // Inferno
                try
                {
                    string infernoWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[1]/span[2]")).Text;
                    string infernoWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[2]/span[2]")).Text;
                    string infernoTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[3]/span[2]")).Text;
                    string infernoRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[4]/span[2]")).Text;
                    string infernoRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[5]/span[2]")).Text;
                    team.Inferno.WinRate = infernoWinsRate;
                    team.Inferno.RoundsPlayed = infernoTotalRounds;
                    team.Inferno.RoundWinRateAfterFirstKill = infernoRoundWinPercentAfterFirstKill;
                    team.Inferno.RoundWinRateAfterFirstDeath = infernoRoundWinPercentAfterFirstDeath;
                }
                catch (NullReferenceException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string infernoBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Inferno.BiggestWin = infernoBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);

                }

                try
                {
                    string infernoBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Inferno.BiggestLoss = infernoBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);

                }

                // Nuke
                try
                {
                    string nukeWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[1]/span[2]")).Text;
                    string nukeWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[2]/span[2]")).Text;
                    string nukeTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[3]/span[2]")).Text;
                    string nukeRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[4]/span[2]")).Text;
                    string nukeRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[5]/span[2]")).Text;
                    team.Nuke.WinRate = nukeWinsRate;
                    team.Nuke.RoundsPlayed = nukeTotalRounds;
                    team.Nuke.RoundWinRateAfterFirstKill = nukeRoundWinPercentAfterFirstKill;
                    team.Nuke.RoundWinRateAfterFirstDeath = nukeRoundWinPercentAfterFirstDeath;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string nukeBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Nuke.BiggestWin = nukeBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);

                }

                try
                {
                    string nukeBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Nuke.BiggestLoss = nukeBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);

                }

                // Vertigo
                try
                {
                    string vertigoWinsDrawsLosses = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[1]/span[2]")).Text;
                    string vertigoWinsRate = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[2]/span[2]")).Text;
                    string vertigoTotalRounds = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[3]/span[2]")).Text;
                    string vertigoRoundWinPercentAfterFirstKill = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[4]/span[2]")).Text;
                    string vertigoRoundWinPercentAfterFirstDeath = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[5]/span[2]")).Text;
                    team.Vertigo.WinRate = vertigoWinsRate;
                    team.Vertigo.RoundsPlayed = vertigoTotalRounds;
                    team.Vertigo.RoundWinRateAfterFirstKill = vertigoRoundWinPercentAfterFirstKill;
                    team.Vertigo.RoundWinRateAfterFirstDeath = vertigoRoundWinPercentAfterFirstDeath;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string vertigoBiggestWin = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Vertigo.BiggestWin = vertigoBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);

                }

                try
                {
                    string vertigoBiggestDefeat = driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Vertigo.BiggestLoss = vertigoBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);

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
                            string name = item.FindElement(By.CssSelector("td.playerCol.bold a")).Text;
                            string maps = item.FindElement(By.CssSelector("td.statsDetail")).Text;
                            string rounds = item.FindElement(By.CssSelector("td.statsDetail.gtSmartphone-only")).Text;
                            string rating2 = item.FindElement(By.CssSelector(".ratingCol")).Text;
                            TeamPlayer player = new TeamPlayer
                            {
                                name = name,
                                rating = rating2,
                                maps = maps
                            };
                            team.Roster.Add(player);
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

            return team;

            }
            finally
            {
                this.Dispose();
            }
            
       }
    }
}
