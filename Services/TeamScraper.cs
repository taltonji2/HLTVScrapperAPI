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

        public ScrapeResult<Team> Scrape(string name)
        {
            Team team = new Team();
            ScrapeResult<Team> result = new ScrapeResult<Team>(team);
            try
            {               
                this.SearchEntity("team", name);
                string teamUrl = Driver.Url.Replace("#tab-infoBox", "");
                this.ScrapeSummary(team);
                Driver.Navigate().GoToUrl(teamUrl + "#tab-rosterBox");
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

        private void ScrapeSummary(Team team)
        {
            var teamName = Driver.FindElement(By.CssSelector("h1.profile-team-name")).Text;
            team.Summary.Name = teamName;

            var teamCountry = Driver.FindElement(By.CssSelector("img.flag")).GetAttribute("alt");
            team.Summary.Country = teamCountry;

            ReadOnlyCollection<IWebElement> profileTeamStats = Driver.FindElements(By.CssSelector("div.profile-team-stat"));
            var teamRank = int.Parse(profileTeamStats[0].FindElement(By.CssSelector("a")).Text.Substring(1));
            team.Summary.Rank = teamRank;

            var teamWeeksInTop30Core = int.Parse(profileTeamStats[1].FindElement(By.CssSelector("span")).Text.Trim());
            team.Summary.WeeksInTop30Core = teamWeeksInTop30Core;

            var teamCoach = profileTeamStats[2].FindElement(By.CssSelector("span.bold.a-default")).Text.Trim().Replace("'", string.Empty);
            team.Summary.Coach.Name = teamCoach;

            var socials = Driver.FindElement(By.CssSelector("div.socialMediaButtons")).FindElements(By.TagName("a")).ToList();
            socials.ForEach(social => team.Summary.Socials.Add((social.GetAttribute("href").Split(".")[1], social.GetAttribute("href"))));
        }

        private void ScrapeRoster(Team team)
        {

        }

        private void ScrapeMatches(Team team)
        {

        }

        private void ScrapeEvents(Team team)
        {

        }

        private void ScrapeAchievements(Team team)
        {

        }

        private void ScrapeStats(Team team)
        {

        }
    }
}
