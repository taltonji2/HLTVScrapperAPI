using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using HLTVScrapperAPI.Models;
using OpenQA.Selenium.Support.UI;

namespace HLTVScrapperAPI.Services
{
    public class PlayerScraper : Scraper
    {
        public PlayerScraper() : base() {}  

        //TODO: Enhance with filter for time-frame of last month three months etc
        public NestedDictionary Scrape(ScrapeRequest request)
        {
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
            searchInput.SendKeys("s1mple");
            
            Thread.Sleep(TimeSpan.FromSeconds(1));
            
            IWebElement playerItem = driver.FindElement(By.CssSelector
            (
                "div[class='box compact player hoverable stats-search-result']"  // saw fail surprisingly
            ));
            
            playerItem.Click();

            // nickName, country, fullName, teamName, age
            IWebElement nickNameElement = driver.FindElement(By.ClassName("summaryNickname"));
            string nickName = nickNameElement.Text;
            playerStatsDict.Add("nickName", nickName);
            IWebElement summaryElement = driver.FindElement(By.ClassName("summaryInfoContainer"));
            string country = summaryElement.FindElement(By.ClassName("summaryRealname")).FindElement(By.ClassName("flag")).GetAttribute("title").ToString();
            playerStatsDict.Add("county", country);
            string fullName = summaryElement.FindElement(By.ClassName("summaryRealname")).FindElement(By.ClassName("text-ellipsis")).Text;
            playerStatsDict.Add("fullName", fullName);
            string teamName = summaryElement.FindElement(By.ClassName("SummaryTeamname")).FindElement(By.ClassName("text-ellipsis")).Text;
            playerStatsDict.Add("teamName", teamName);
            string age = summaryElement.FindElement(By.ClassName("summaryPlayerAge")).Text;
            playerStatsDict.Add("age", age);

            IWebElement summaryBreakdownElement = driver.FindElement(By.ClassName("summaryBreakdownContainer"));
            ReadOnlyCollection<IWebElement> summaryStatBreakdownElements = summaryBreakdownElement.FindElements(By.ClassName("summaryStatBreakdown"));
            foreach (IWebElement breakdownElement in summaryStatBreakdownElements)
            {
                IWebElement nameElement = breakdownElement.FindElement(By.ClassName("summaryStatBreakdownSubHeader"));
                string statName = nameElement.Text;
                IWebElement valueElement = breakdownElement.FindElement(By.ClassName("summaryStatBreakdownData")).FindElement(By.ClassName("summaryStatBreakdownDataValue"));
                string value = valueElement.Text;
                IWebElement descriptionElement = breakdownElement.FindElement(By.ClassName("summaryStatBreakdownDescription"));
                string description = descriptionElement.Text;
                playerStatsDict.Add(statName, $"{value}_{description}");
            }

            ReadOnlyCollection<IWebElement> statRowElements = driver.FindElements(By.ClassName("stats-row"));
            Dictionary<string, string> generalStatistics = new Dictionary<string, string>();
            PopulateStatsFromSpanLists(statRowElements, generalStatistics);
            playerStatsDict.Add("general", generalStatistics);

            IWebElement featuredRatingGrid = driver.FindElement(By.ClassName("featured-ratings-container")).FindElement(By.ClassName("g-grid"));
            ReadOnlyCollection<IWebElement> featuredRatingStats = featuredRatingGrid.FindElements(By.ClassName("col-custom"));
            Dictionary<string, string> ratingStatistics = new Dictionary<string, string>();
            playerStatsDict.Add("rating", ratingStatistics);
            foreach (IWebElement stat in featuredRatingStats)
            {
                IWebElement ratingBreakdown = stat.FindElement(By.ClassName("rating-breakdown"));
                string ratingValue = ratingBreakdown.FindElement(By.ClassName("rating-value")).Text;
                string ratingDescription = ratingBreakdown.FindElement(By.ClassName("rating-description")).Text;
                string ratingMaps = ratingBreakdown.FindElement(By.ClassName("rating-maps")).Text;
                ratingStatistics.Add($"RATING 2.0 {ratingDescription}", $"{ratingValue}_{ratingMaps}");
            }

            //Navigate to Individual tab
            IWebElement individualTab = driver.FindElement(By.LinkText("Individual"));
            individualTab.Click();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            ReadOnlyCollection<IWebElement> standardBoxElements = driver.FindElements(By.XPath("//*[@class='standard-box']"));

            IWebElement overallStatsBox = standardBoxElements[0];
            ReadOnlyCollection<IWebElement> overallStatRowElements = overallStatsBox.FindElements(By.ClassName("stats-row"));
            Dictionary<string, string> overallStats = new Dictionary<string, string>();
            PopulateStatsFromSpanLists(overallStatRowElements, overallStats);
            playerStatsDict.Add("overall", overallStats);

            IWebElement openingStatsBox = standardBoxElements[1];
            ReadOnlyCollection<IWebElement> openingStatRowElements = openingStatsBox.FindElements(By.ClassName("stats-row"));
            Dictionary<string, string> roundStats = new Dictionary<string, string>();
            PopulateStatsFromSpanLists(openingStatRowElements, roundStats);
            playerStatsDict.Add("round", roundStats);

            IWebElement roundStatsBox = standardBoxElements[2];
            ReadOnlyCollection<IWebElement> roundStatRowElements = roundStatsBox.FindElements(By.ClassName("stats-row"));
            Dictionary<string, string> openingStats = new Dictionary<string, string>();
            PopulateStatsFromSpanLists(roundStatRowElements, openingStats);
            playerStatsDict.Add("opening", openingStats);

            IWebElement weaponStatsBox = standardBoxElements[3];
            ReadOnlyCollection<IWebElement> weaponStatRowElements = weaponStatsBox.FindElements(By.ClassName("stats-row"));
            Dictionary<string, string> weaponStats = new Dictionary<string, string>();
            PopulateStatsFromSpanLists(weaponStatRowElements, weaponStats);
            playerStatsDict.Add("weapon", weaponStats);

            void PopulateStatsFromSpanLists(ReadOnlyCollection<IWebElement> webElementArray, Dictionary<string, string> stat)
            {
                foreach (IWebElement webElement in webElementArray)
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

                        stat.Add(statName, statValue);
                    }
                }
            }

            return playerStatsDict;
            
            }
            catch
            {
                throw;
            }
            finally
            {
                this.Dispose();
            }
        }
    }
}
