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

        public Team Scrape(TeamScrapeRequest request)
        {
            try 
            { 
                Team team = new Team();
                Driver.Navigate().GoToUrl($"https://www.hltv.org/stats");
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

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
                    IWebElement searchInput = Driver.FindElement(By.CssSelector("input[class='search-input navsearchinput tt-input']"));
                    searchInput.Click();
                    searchInput.SendKeys(request.Name);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error: Search input not found {e.Message}");
                }

                try
                {
                    IWebElement teamItem = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[2]/div[1]/div[2]/div[2]/div/div/div/span/div/div/div/div[2]/div[1]/a[1]"));
                    teamItem.Click();
                    IWebElement currentContext = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[10]/a"));
                    currentContext.Click();
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error: Team item not found {e.Message}");
                    return null;
                }

                try
                {
                    string mapsPlayed = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[6]/div[1]/div[1]")).Text;
                    team.MapsPlayed = mapsPlayed;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string winsDrawsLosses = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[6]/div[2]/div[1]")).Text;
                    //calculate win rate
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string roundsPlayed = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[8]/div[2]/div[1]")).Text;
                    team.RoundsPlayed = roundsPlayed;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string killDeathRatio = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[8]/div[3]/div[1]")).Text;
                    team.KD = killDeathRatio;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                //Maps
                try
                {
                    IWebElement mapsTab = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[4]/div/div[1]/a[3]"));
                    mapsTab.Click();
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                // Anubis
                try
                {
                    string anubisWinsDrawsLosses = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[2]/div[1]/span[2]")).Text;
                    string anubisWinsRate = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[2]/div[2]/span[2]")).Text;
                    string anubisTotalRounds = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[2]/div[3]/span[2]")).Text;
                    string anubisRoundWinPercentAfterFirstKill = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[2]/div[4]/span[2]")).Text;
                    string anubisRoundWinPercentAfterFirstDeath = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[2]/div[5]/span[2]")).Text;
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
                    string anubisBiggestWin = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Anubis.BiggestWin = anubisBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string anubisBiggestDefeat = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[1]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Anubis.BiggestLoss = anubisBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                // Ancient
                try
                {
                    string ancientWinsDrawsLosses = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[1]/span[2]")).Text;
                    string ancientWinsRate = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[2]/span[2]")).Text;
                    string ancientTotalRounds = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[3]/span[2]")).Text;
                    string ancientRoundWinPercentAfterFirstKill = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[4]/span[2]")).Text;
                    string ancientRoundWinPercentAfterFirstDeath = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[2]/div[5]/span[2]")).Text;
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
                    string ancientBiggestWin = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Ancient.BiggestWin = ancientBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string ancientBiggestDefeat = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[2]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Ancient.BiggestLoss = ancientBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                // Overpass
                try
                {
                    string overpassWinsDrawsLosses = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[1]/span[2]")).Text;
                    string overpassWinsRate = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[2]/span[2]")).Text;
                    string overpassTotalRounds = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[3]/span[2]")).Text;
                    string overpassRoundWinPercentAfterFirstKill = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[4]/span[2]")).Text;
                    string overpassRoundWinPercentAfterFirstDeath = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[2]/div[5]/span[2]")).Text;
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
                    string overpassBiggestWin = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Overpass.BiggestWin = overpassBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string overpassBiggestDefeat = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[3]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Overpass.BiggestLoss = overpassBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                // Mirage 
                try
                {
                    string mirageWinsDrawsLosses = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[1]/span[2]")).Text;
                    string mirageWinsRate = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[2]/span[2]")).Text;
                    string mirageTotalRounds = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[3]/span[2]")).Text;
                    string mirageRoundWinPercentAfterFirstKill = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[4]/span[2]")).Text;
                    string mirageRoundWinPercentAfterFirstDeath = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[2]/div[5]/span[2]")).Text;
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
                    string mirageBiggestWin = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Mirage.BiggestWin = mirageBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string mirageBiggestDefeat = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[4]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Mirage.BiggestLoss = mirageBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                // Inferno
                try
                {
                    string infernoWinsDrawsLosses = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[1]/span[2]")).Text;
                    string infernoWinsRate = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[2]/span[2]")).Text;
                    string infernoTotalRounds = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[3]/span[2]")).Text;
                    string infernoRoundWinPercentAfterFirstKill = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[4]/span[2]")).Text;
                    string infernoRoundWinPercentAfterFirstDeath = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[2]/div[5]/span[2]")).Text;
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
                    string infernoBiggestWin = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Inferno.BiggestWin = infernoBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);

                }

                try
                {
                    string infernoBiggestDefeat = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[5]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Inferno.BiggestLoss = infernoBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);

                }

                // Nuke
                try
                {
                    string nukeWinsDrawsLosses = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[1]/span[2]")).Text;
                    string nukeWinsRate = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[2]/span[2]")).Text;
                    string nukeTotalRounds = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[3]/span[2]")).Text;
                    string nukeRoundWinPercentAfterFirstKill = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[4]/span[2]")).Text;
                    string nukeRoundWinPercentAfterFirstDeath = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[2]/div[5]/span[2]")).Text;
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
                    string nukeBiggestWin = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Nuke.BiggestWin = nukeBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);

                }

                try
                {
                    string nukeBiggestDefeat = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[6]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Nuke.BiggestLoss = nukeBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);

                }

                // Vertigo
                try
                {
                    string vertigoWinsDrawsLosses = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[1]/span[2]")).Text;
                    string vertigoWinsRate = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[2]/span[2]")).Text;
                    string vertigoTotalRounds = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[3]/span[2]")).Text;
                    string vertigoRoundWinPercentAfterFirstKill = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[4]/span[2]")).Text;
                    string vertigoRoundWinPercentAfterFirstDeath = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[2]/div[5]/span[2]")).Text;
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
                    string vertigoBiggestWin = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[3]/div[1]/a/div/div[1]/div[2]")).Text;
                    team.Vertigo.BiggestWin = vertigoBiggestWin;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    string vertigoBiggestDefeat = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[9]/div[9]/div[3]/div[2]/a/div/div[1]/div[2]")).Text;
                    team.Vertigo.BiggestLoss = vertigoBiggestDefeat;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);

                }

                //Players
                try
                {
                    IWebElement playersTab = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/div[4]/div/div[1]/a[4]"));
                    playersTab.Click();
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine(e.Message);
                }

                try
                {
                    IWebElement playersTableDiv = Driver.FindElement(By.XPath("/html/body/div[2]/div[8]/div[2]/div[1]/div[2]/table/tbody"));
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
