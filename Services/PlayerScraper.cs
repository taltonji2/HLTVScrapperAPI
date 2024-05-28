using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using HLTVScrapperAPI.Models;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace HLTVScrapperAPI.Services
{
    public class PlayerScraper : Scraper
    {
        public PlayerScraper() : base() {}

        void PopulateStatsFromSpanLists(ReadOnlyCollection<IWebElement> webElementArray, Player player)
        {
            foreach (IWebElement webElement in webElementArray)
            {
                try
                {
                    ReadOnlyCollection<IWebElement> spanElements = webElement.FindElements(By.XPath(".//*"));
                    if (spanElements.Count == 2)
                    {
                        string statName = spanElements[0].Text;
                        string statValue = spanElements[1].Text;
                        bool containsNumericOrEndsWithPercentage = Regex.IsMatch(statName, @"^[-+]?\d*\.?\d+%?$");

                        if (containsNumericOrEndsWithPercentage)
                        {
                            (statName, statValue) = (statValue, statName);
                        }

                        switch (statName)
                        {
                            case "Total kills":
                                player.General.TotalKills = statValue;
                                break;
                            case "Headshot %":
                                player.General.HeadshotPercentage = statValue;
                                break;
                            case "Total deaths":
                                player.General.TotalDeaths = statValue;
                                break;
                            case "K/D Ratio":
                                player.General.KD = statValue;
                                break;
                            case "Damage / Round":
                                player.General.DmgPerRound = statValue;
                                break;
                            case "Grenade dmg / Round":
                                player.General.GrenadeDmgPerRound = statValue;
                                break;
                            case "Maps played":
                                player.General.MapsPlayed = statValue;
                                break;  
                            case "Rounds played":
                                player.General.RoundsPlayed = statValue;
                                break;  
                            case "Kills / round":
                                player.General.KillsPerRound = statValue;
                                break;
                            case "Assists / round":
                                player.General.AssistsPerRound = statValue;
                                break;
                            case "Deaths / round":
                                player.General.DeathsPerRound = statValue;
                                break;
                            case "Saved by teammate / round":
                                player.General.SavedPerRound = statValue;
                                break;
                            case "Saved teammates / round":
                                player.General.SavesPerRound = statValue;
                                break;
                        }
                    }
                } 
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error: {e.Message}");
                }
            }
        }
        //TODO: Enhance with filter for time-frame of last month three months etc
        public Player Scrape(ScrapeRequest request)
        {
            try
            {
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

                Player player = new Player();
                
                try
                {
                    IWebElement searchInput = driver.FindElement(By.CssSelector("input[class='search-input navsearchinput tt-input']"));
                    searchInput.Click();
                    searchInput.SendKeys("s1mple");
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error: {e.Message}");
                }

                try
                {
                    IWebElement playerItem = driver.FindElement(By.CssSelector("div[class='box compact player hoverable stats-search-result']"));
                    playerItem.Click();
                } 
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error: {e.Message}");
                }
                
                // NickName, country, FullName, TeamName, Age
                try
                {
                    IWebElement nickNameElement = driver.FindElement(By.ClassName("summaryNickname"));
                    string nickName = nickNameElement.Text;
                    player.NickName = nickName;
                } 
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error: {e.Message}");
                }

                try
                {
                    IWebElement summaryElement = driver.FindElement(By.ClassName("summaryInfoContainer"));
                    string country = summaryElement.FindElement(By.ClassName("summaryRealname")).FindElement(By.ClassName("flag")).GetAttribute("title").ToString();
                    player.County = country;
                    string fullName = summaryElement.FindElement(By.ClassName("summaryRealname")).FindElement(By.ClassName("text-ellipsis")).Text;
                    player.FullName = fullName;
                    string teamName = summaryElement.FindElement(By.ClassName("SummaryTeamname")).FindElement(By.ClassName("text-ellipsis")).Text;
                    player.TeamName = teamName;
                    string age = summaryElement.FindElement(By.ClassName("summaryPlayerAge")).Text;
                    player.Age = age;
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error: {e.Message}");
                }

                try
                {
                    IWebElement summaryBreakdownElement = driver.FindElement(By.ClassName("summaryBreakdownContainer"));
                    ReadOnlyCollection<IWebElement> summaryStatBreakdownElements = summaryBreakdownElement.FindElements(By.ClassName("summaryStatBreakdown"));
                    foreach (IWebElement breakdownElement in summaryStatBreakdownElements)
                    {
                        string stat = breakdownElement.FindElement(By.ClassName("summaryStatBreakdownSubHeader")).Text;
                        string statValue = breakdownElement.FindElement(By.ClassName("summaryStatBreakdownData")).FindElement(By.ClassName("summaryStatBreakdownDataValue")).Text;
                        string description = breakdownElement.FindElement(By.ClassName("summaryStatBreakdownDescription")).Text;
                        switch (stat)
                        {
                            case "Rating 2.0":
                                player.Rating1_0 = statValue;
                                break;
                            case "DPR":
                                player.DPR = statValue;
                                break;
                            case "KAST":
                                player.KAST = statValue;
                                break;
                            case "Impact":
                                player.Impact = statValue;
                                break;
                            case "ADR":
                                player.ADR = statValue;
                                break;
                            case "KPR":
                                player.KPR = statValue;
                                break;
                        }
                    }
                } 
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error: {e.Message}");
                }
                
                try
                {
                    ReadOnlyCollection<IWebElement> statRowElements = driver.FindElements(By.ClassName("stats-row"));
                    PopulateStatsFromSpanLists(statRowElements, player);                
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error: {e.Message}");
                }

                try
                {
                    IWebElement featuredRatingGrid = driver.FindElement(By.ClassName("featured-ratings-container")).FindElement(By.ClassName("g-grid"));
                    ReadOnlyCollection<IWebElement> featuredRatingStats = featuredRatingGrid.FindElements(By.ClassName("col-custom"));
                    foreach (IWebElement stat in featuredRatingStats)
                    {
                        try
                        {
                            IWebElement ratingBreakdown = stat.FindElement(By.ClassName("rating-breakdown"));
                            string ratingValue = ratingBreakdown.FindElement(By.ClassName("rating-value")).Text;
                            string ratingDescription = ratingBreakdown.FindElement(By.ClassName("rating-description")).Text;
                            string ratingMaps = ratingBreakdown.FindElement(By.ClassName("rating-maps")).Text;
                            switch (ratingDescription)
                            {
                                case "vs top 5 opponents":
                                    player.OpponentRating.RatingTop5.rating = ratingValue;
                                    player.OpponentRating.RatingTop5.maps = ratingMaps;
                                    break;
                                case "vs top 10 opponents":
                                    player.OpponentRating.RatingTop10.rating = ratingValue;
                                    player.OpponentRating.RatingTop10.maps = ratingMaps;
                                    break;
                                case "vs top 20 opponents":
                                    player.OpponentRating.RatingTop20.rating = ratingValue;
                                    player.OpponentRating.RatingTop20.maps = ratingMaps;
                                    break;
                                case "vs top 30 opponents":
                                    player.OpponentRating.RatingTop30.rating = ratingValue;
                                    player.OpponentRating.RatingTop30.maps = ratingMaps;
                                    break;
                                case "vs top 50 opponents":
                                    player.OpponentRating.RatingTop50.rating = ratingValue;
                                    player.OpponentRating.RatingTop50.maps = ratingMaps;
                                    break;
                            }
                        }
                        catch (NoSuchElementException e)
                        {
                            Debug.WriteLine($"Error: {e.Message}");
                        }
                    }
                } 
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error: {e.Message}");
                }

                //Navigate to Individual tab
                try 
                {
                    IWebElement individualTab = driver.FindElement(By.LinkText("Individual"));
                    individualTab.Click();
                }
                catch (NoSuchElementException e)
                {
                    Debug.WriteLine($"Error: {e.Message}");
                }
                
                return player;
            
            }
            finally
            {
                this.Dispose();
            }
        }
    }
}
