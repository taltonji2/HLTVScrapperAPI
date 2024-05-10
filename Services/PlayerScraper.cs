using OpenQA.Selenium;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace HLTVScrapperAPI.Services
{
    public class PlayerScraper : Scraper
    {
        private static void ScrapePlayerStatsAction(IWebDriver driver)
        {
            IWebElement searchInput = driver.FindElement(By.CssSelector
            (
                "input[class='search-input navsearchinput tt-input']"
            ));
            
            searchInput.Click();
            searchInput.SendKeys("s1mple");
            
            Thread.Sleep(TimeSpan.FromSeconds(2));
            
            IWebElement playerItem = driver.FindElement(By.CssSelector
            (
                "div[class='box compact player hoverable stats-search-result']"  //saw fail surprisingly
            ));
            
            playerItem.Click();

            IWebElement nickNameElement = driver.FindElement(By.ClassName("summaryNickname"));
            string nickName = nickNameElement.Text;

            IWebElement summaryElement = driver.FindElement(By.ClassName("summaryInfoContainer"));

            string country = summaryElement.FindElement(By.ClassName("summaryRealname")).FindElement(By.ClassName("flag")).GetAttribute("title").ToString();
            Debug.WriteLine(country);

            string fullName = summaryElement.FindElement(By.ClassName("summaryRealname")).FindElement(By.ClassName("text-ellipsis")).Text;
            Debug.WriteLine(fullName);

            string teamName = summaryElement.FindElement(By.ClassName("SummaryTeamname")).FindElement(By.ClassName("text-ellipsis")).Text;
            Debug.WriteLine(teamName);

            string age = summaryElement.FindElement(By.ClassName("summaryPlayerAge")).Text;
            Debug.WriteLine(age);

            IWebElement summaryBreakdownElement = driver.FindElement(By.ClassName("summaryBreakdownContainer"));

            ReadOnlyCollection<IWebElement> summaryStatBreakdownElements = summaryBreakdownElement.FindElements(By.ClassName("summaryStatBreakdown"));

            Dictionary<string, (string value, string description)> playerSummaryStatistics = new Dictionary<string, (string, string)>();

            foreach (IWebElement breakdownElement in summaryStatBreakdownElements)
            {
                IWebElement nameElement = breakdownElement.FindElement(By.ClassName("summaryStatBreakdownSubHeader"));
                string statName = nameElement.Text;

                IWebElement valueElement = breakdownElement.FindElement(By.ClassName("summaryStatBreakdownData")).FindElement(By.ClassName("summaryStatBreakdownDataValue"));
                string value = valueElement.Text;

                IWebElement descriptionElement = breakdownElement.FindElement(By.ClassName("summaryStatBreakdownDescription"));
                string description = descriptionElement.Text;

                playerSummaryStatistics.Add(statName, (value, description));
            }

            foreach(var item in  playerSummaryStatistics) 
            {
                Debug.WriteLine(item);
            }

            ReadOnlyCollection<IWebElement> statRowElements = driver.FindElements(By.ClassName("stats-row"));

            Dictionary<string, string> playerStatistics = new Dictionary<string, string>();

            PopulateStatsFromSpanLists(statRowElements, playerStatistics);

            foreach(var item in playerStatistics)
            {
                Debug.WriteLine(item);
            }

            Dictionary<string, string> playerFeaturedRatings = new Dictionary<string, string>();

            IWebElement featuredRatingGrid = driver.FindElement(By.ClassName("featured-ratings-container")).FindElement(By.ClassName("g-grid"));

            ReadOnlyCollection<IWebElement> featuredRatingStats = featuredRatingGrid.FindElements(By.ClassName("col-custom"));

            foreach (IWebElement stat in featuredRatingStats)
            {
                IWebElement ratingBreakdown = stat.FindElement(By.ClassName("rating-breakdown"));
                
                string ratingValue = ratingBreakdown.FindElement(By.ClassName("rating-value")).Text;
                string ratingDescription = ratingBreakdown.FindElement(By.ClassName("rating-description")).Text;
                string ratingMaps = ratingBreakdown.FindElement(By.ClassName("rating-maps")).Text;
                ratingDescription = $"{ratingDescription}_{ratingMaps}";
                playerFeaturedRatings.Add(ratingDescription, ratingValue);
            }

            foreach(var item in playerFeaturedRatings) 
            {
                Debug.WriteLine(item);
            }

            //Navigate to Individual tab
            IWebElement individualTab = driver.FindElement(By.LinkText("Individual"));
            
            individualTab.Click();

            Thread.Sleep(2000);

            Dictionary<string, string> overallStats = new Dictionary<string, string>();
            Dictionary<string, string> roundStats = new Dictionary<string, string>();
            Dictionary<string, string> openingStats = new Dictionary<string, string>();
            Dictionary<string, string> weaponStats = new Dictionary<string, string>();

            ReadOnlyCollection<IWebElement> standardBoxElements = driver.FindElements(By.XPath("//*[@class='standard-box']"));

            IWebElement overallStatsBox = standardBoxElements[0];
            IWebElement openingStatsBox = standardBoxElements[1];
            IWebElement roundStatsBox = standardBoxElements[2];
            IWebElement weaponStatsBox = standardBoxElements[3];

            ReadOnlyCollection<IWebElement> overallStatRowElements = overallStatsBox.FindElements(By.ClassName("stats-row"));
            ReadOnlyCollection<IWebElement> openingStatRowElements = openingStatsBox.FindElements(By.ClassName("stats-row"));
            ReadOnlyCollection<IWebElement> roundStatRowElements = roundStatsBox.FindElements(By.ClassName("stats-row"));
            ReadOnlyCollection<IWebElement> weaponStatRowElements = weaponStatsBox.FindElements(By.ClassName("stats-row"));

            PopulateStatsFromSpanLists(overallStatRowElements, overallStats);
            PopulateStatsFromSpanLists(openingStatRowElements, roundStats);
            PopulateStatsFromSpanLists(roundStatRowElements, openingStats);
            PopulateStatsFromSpanLists(weaponStatRowElements, weaponStats);

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

            foreach (var item in overallStats)
            {
                Debug.WriteLine(item);
            }

            foreach (var item in roundStats)
            {
                Debug.WriteLine(item);
            }
            foreach (var item in openingStats)
            {
                Debug.WriteLine(item);
            }
            foreach (var item in weaponStats)
            {
                Debug.WriteLine(item);
            }
        }

        public static void ScrapePlayerStats()
        {
            Scrape(route: "stats", action: ScrapePlayerStatsAction);
        }
    }
}
