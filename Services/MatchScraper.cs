using HLTVScrapperAPI.Models;
using OpenQA.Selenium;

namespace HLTVScrapperAPI.Services
{
    public class MatchScraper : Scraper
    {
        public MatchScraper() : base() {}

        public ScrapeResult<List<Match>> ScrapeAll()
        {
            List<Match> matches = [];
            ScrapeResult<List<Match>> result = new ScrapeResult<List<Match>>(matches);
            try
            {
                Driver.Navigate().GoToUrl("https://www.hltv.org/matches");
                this.ScrapeMatches(matches);
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                result.Success = false;
            }
            finally
            {
                this.DisposeDriver();
            }
            return result;
        }
        
        public ScrapeResult<List<Match>> ScrapeTopTier()
        {
            List<Match> matches = [];
            ScrapeResult<List<Match>> result = new ScrapeResult<List<Match>>(matches);
            try
            {
                Driver.Navigate().GoToUrl("https://www.hltv.org/matches?predefinedFilter=top_tier");
                this.ScrapeMatches(matches);
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                result.Success = false;
            }
            finally
            {
                this.DisposeDriver();
            }
            return result;
        }
        
        public ScrapeResult<List<Match>> ScrapeLAN()
        {
            List<Match> matches = [];
            ScrapeResult<List<Match>> result = new ScrapeResult<List<Match>>(matches);
            try
            {
                Driver.Navigate().GoToUrl("https://www.hltv.org/matches?predefinedFilter=lan_only");
                this.ScrapeMatches(matches);
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                result.Success = false;
            }
            finally
            {
                this.DisposeDriver();
            }
            return result;
        }

        private void ScrapeMatches(List<Match> matches)
        {
            try
            {
                var noUpcomingMatches = Driver.FindElement(By.CssSelector("div.newMatchesEmptystateContainer"));
            }
            catch (NoSuchElementException)
            {
                var upcomingMatchesContainer = Driver.FindElement(By.CssSelector("div.upcomingMatchesAll"));
                var upcomingMatchesSections = upcomingMatchesContainer.FindElements(By.CssSelector("div.upcomingMatchesSection"));
                foreach (var section in upcomingMatchesSections)
                {
                    var matchDayHeadline = section.FindElement(By.CssSelector("div.matchDayHeadline")).Text;
                    string[] dateParts = matchDayHeadline.Split("-");
                    foreach (var item in dateParts)
                    {
                        item.Trim();
                    }
                    DateTime date = DateTime.Parse(dateParts[1] + "-" + dateParts[2] + "-" + dateParts[3]);
                    var upcomingMatches = section.FindElements(By.CssSelector("a.match.a-reset"));
                    foreach (var match in upcomingMatches)
                    {
                        Driver.Navigate().GoToUrl(match.GetAttribute("href"));
                        Match newMatch = new Match();
                        newMatch.Date = date;
                        newMatch.Time = DateTime.Parse(match.FindElement(By.CssSelector("div.time")).Text);
                        newMatch.Event = match.FindElement(By.CssSelector("div.event.text-ellipsis")).Text;
                        try
                        {
                            newMatch.Team1 = match.FindElement(By.CssSelector("div.team1-gradient")).FindElement(By.CssSelector("div.teamName")).Text;
                            newMatch.Team1 = match.FindElement(By.CssSelector("div.team2-gradient")).FindElement(By.CssSelector("div.teamName")).Text;
                        }
                        catch (NoSuchElementException)
                        {
                            newMatch.Team1 = "TBD";
                            newMatch.Team2 = "TBD";
                        }
                        matches.Add(newMatch);
                    }
                }
            }
        }
    }
}
