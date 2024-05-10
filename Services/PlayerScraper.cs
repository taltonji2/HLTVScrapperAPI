using OpenQA.Selenium;
using SeleniumUndetectedChromeDriver;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

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

            IWebElement nickNameElement = driver
                .FindElement(By.ClassName("summaryNickname"));
            string nickName = nickNameElement.Text;

            IWebElement summaryElement = driver
                .FindElement(By.ClassName("summaryInfoContainer"));

            string country = summaryElement
                .FindElement(By.ClassName("summaryRealname"))
                .FindElement(By.ClassName("flag"))
                .GetAttribute("title").ToString();
            Debug.WriteLine(country);

            string fullName = summaryElement
                .FindElement(By.ClassName("summaryRealname"))
                .FindElement(By.ClassName("text-ellipsis"))
                .Text;
            Debug.WriteLine(fullName);

            string teamName = summaryElement
                .FindElement(By.ClassName("SummaryTeamname"))
                .FindElement(By.ClassName("text-ellipsis"))
                .Text;
            Debug.WriteLine(teamName);

            string age = summaryElement
                .FindElement(By.ClassName("summaryPlayerAge"))
                .Text;
            Debug.WriteLine(age);

            IWebElement summaryBreakdownElement = driver
                .FindElement(By.ClassName("summaryBreakdownContainer"));

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> summaryStatBreakdownElements = summaryBreakdownElement.FindElements(By.ClassName("summaryStatBreakdown"));

            Dictionary<string, (string value, string description)> playerSummaryStatistics = new Dictionary<string, (string, string)>();

            foreach (IWebElement breakdownElement in summaryStatBreakdownElements)
            {
                IWebElement nameElement = breakdownElement
                    .FindElement(By.ClassName("summaryStatBreakdownSubHeader"));
                string statName = nameElement.Text;

                IWebElement valueElement = breakdownElement
                    .FindElement(By.ClassName("summaryStatBreakdownData"))
                    .FindElement(By.ClassName("summaryStatBreakdownDataValue"));
                string value = valueElement.Text;

                IWebElement descriptionElement = breakdownElement
                    .FindElement(By.ClassName("summaryStatBreakdownDescription"));
                string description = descriptionElement.Text;

                playerSummaryStatistics.Add(statName, (value, description));
            }

            foreach(var item in  playerSummaryStatistics) 
            {
                Debug.WriteLine(item);
            }

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> statRowElements = driver.FindElements(By.ClassName("stats-row"));

            Dictionary<string, string> playerStatistics = new Dictionary<string, string>();

            foreach (IWebElement statRow in statRowElements)
            {
                System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> spanElements = statRow.FindElements(By.XPath(".//*"));

                if (spanElements.Count == 2)
                {
                    string statName = spanElements[0].Text;
                    string statValue = spanElements[1].Text;

                    bool containsNumericOrEndsWithPercentage = Regex.IsMatch(statName, @"^[-+]?\d*\.?\d+%?$");

                    if (containsNumericOrEndsWithPercentage)
                    {
                        (statName, statValue) = (statValue, statName);
                    }

                    playerStatistics.Add(statName, statValue);
                }
            }

            Dictionary<string, string> playerFeaturedRatings = new Dictionary<string, string>();

            IWebElement featuredRatingGrid = driver
                .FindElement(By.ClassName("featured-ratings-container"))
                .FindElement(By.ClassName("g-grid"));

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> featuredRatingStats = featuredRatingGrid.FindElements(By.ClassName("col-custom"));

            foreach (IWebElement stat in featuredRatingStats)
            {
                IWebElement ratingBreakdown = stat.FindElement(By.ClassName("rating-breakdown"));
                string ratingValue = ratingBreakdown.FindElement(By.ClassName("rating-value")).Text;
                string ratingDescription = ratingBreakdown
                    .FindElement(By.ClassName("rating-description"))
                    .Text;
                string ratingMaps = ratingBreakdown.FindElement(By.ClassName("rating-maps")).Text;

                ratingDescription = $"{ratingDescription}_{ratingMaps}";

                playerFeaturedRatings.Add(ratingDescription, ratingValue);
            }

            foreach(var item in playerFeaturedRatings) 
            {
                Debug.WriteLine(item);
            }

            IWebElement individualTab = driver.FindElement(By.LinkText("Individual"));
            individualTab.Click();

            Thread.Sleep(2000);

            Dictionary<string, string> overallStats = new Dictionary<string, string>();
            Dictionary<string, string> roundStats = new Dictionary<string, string>();
            Dictionary<string, string> openingStats = new Dictionary<string, string>();
            Dictionary<string, string> weaponStats = new Dictionary<string, string>();
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> standardBoxElements = driver.FindElements(By.XPath("//*[@class='standard-box']"));

            IWebElement overallStatsBox = standardBoxElements[0];
            IWebElement openingStatsBox = standardBoxElements[1];
            IWebElement roundStatsBox = standardBoxElements[2];
            IWebElement weaponStatsBox = standardBoxElements[3];

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> overallStatRowElements = overallStatsBox.FindElements(By.ClassName("stats-row"));
            //GetStatRowData(overallStatRowElements, overallStats);

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> openingStatRowElements = openingStatsBox.FindElements(By.ClassName("stats-row"));
            //GetStatRowData(openingStatRowElements, openingStats);

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> roundStatRowElements = roundStatsBox.FindElements(By.ClassName("stats-row"));
            //GetStatRowData(roundStatRowElements, roundStats);

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> weaponStatRowElements = weaponStatsBox.FindElements(By.ClassName("stats-row"));
            //GetStatRowData(weaponStatRowElements, weaponStats);

            IWebElement matchTab = driver.FindElement(By.CssSelector("div['class=tabs standard-box']")).FindElement(By.LinkText("Matches"));
            matchTab.Click();

            Thread.Sleep(2000);

            IWebElement matchTableElement = driver.FindElement(By.CssSelector("table[class='stats-table sortable-table stats-matches-table']"));

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> group1Elements = matchTableElement.FindElements(By.CssSelector("tr.group-1"));
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> group2Elements = matchTableElement.FindElements(By.CssSelector("tr.group-2"));

            List<IWebElement> matchTableRowElements = new List<IWebElement>();
            matchTableRowElements.AddRange(group1Elements);
            matchTableRowElements.AddRange(group2Elements);

            Console.WriteLine($"{matchTableRowElements.Count} match table row elements");

            var matchHistory = new List<Dictionary<string, string>>();
            foreach (IWebElement rowElement in matchTableRowElements)
            {
                var rowStats = new Dictionary<string, string>();

                string date = rowElement.FindElement(By.CssSelector("td div.time")).Text;
                string playerTeam = rowElement.FindElement(By.CssSelector("td div.gtSmartphone-only span")).Text;
                string opponentTeam = rowElement.FindElement(By.CssSelector("td:nth-child(3) div.gtSmartphone-only span")).Text;
                string map = rowElement.FindElement(By.CssSelector("td.statsMapPlayed")).Text;
                string kd = rowElement.FindElement(By.CssSelector("td.statsCenterText")).Text;
                string dif = rowElement.FindElement(By.CssSelector("td.gtSmartphone-only.centerStat")).Text;

                var possibleClasses = new List<string>
                {
                    "td.match-lost.ratingNeutral",
                    "td.match-won.ratingNeutral",
                    "td.match-lost.ratingNegative",
                    "td.match-won.ratingPositive"
                };

                string rating = "";
                foreach (string classVal in possibleClasses)
                {
                    if (rowElement.FindElements(By.CssSelector(classVal)).Count > 0)
                    {
                        rating = rowElement.FindElement(By.CssSelector(classVal)).Text;
                        break;
                    }
                }

                rowStats.Add("date", date);
                rowStats.Add("player_team", playerTeam);
                rowStats.Add("opponent_team", opponentTeam);
                rowStats.Add("map", map);
                rowStats.Add("k_d", kd);
                rowStats.Add("dif", dif);
                rowStats.Add("rating", rating);

                matchHistory.Add(rowStats);
            }
            
            IWebElement matchesSummaryElement = driver.FindElement(By.ClassName("summary"));
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> statColElements = matchesSummaryElement.FindElements(By.XPath("//*[@class='col']"));
            
            string averageRating = statColElements[0].Text;
            string mapsWon = statColElements[1].Text;
            string mapsWonWithPositiveRating = statColElements[2].Text;

            Console.WriteLine(matchHistory);
            Console.WriteLine($"{averageRating} {mapsWon} {mapsWonWithPositiveRating}");
        }

        public static void ScrapePlayerStats()
        {
            Scrape(route: "stats", action: ScrapePlayerStatsAction);
        }
    }
}
