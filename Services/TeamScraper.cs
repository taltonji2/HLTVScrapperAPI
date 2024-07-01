using System.Collections.ObjectModel;
using HLTVScrapperAPI.Models;
using OpenQA.Selenium;
using HLTVScrapperAPI.Models.Team;
using System.Text.RegularExpressions;
using System.Diagnostics.Metrics;
using System.Diagnostics;

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
                this.SearchScrapeObject("team", name);
                string teamUrl = Driver.Url.Replace("#tab-infoBox", "");
                this.ScrapeSummary(team);
                Driver.Navigate().GoToUrl(teamUrl + "#tab-rosterBox");
                this.ScrapeRoster(team);

                //Additional
                Driver.Navigate().GoToUrl(teamUrl + "#tab-matchesBox");
                this.ScrapeMatches(team);
                Driver.Navigate().GoToUrl(teamUrl + "#tab-eventsBox");
                this.ScrapeEvents(team);
                Driver.Navigate().GoToUrl(teamUrl + "#tab-achievementsBox");
                this.ScrapeAchievements(team);
                Driver.Navigate().GoToUrl(teamUrl + "#tab-statsBox");
                this.ScrapeStats(team);
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

            List<IWebElement> profileTeamStats = Driver.FindElements(By.CssSelector("div.profile-team-stat")).ToList();
            
            var teamRank = profileTeamStats[0].FindElement(By.CssSelector("span.right")).Text;
            if (!teamRank.Equals("-"))
            {
                team.Summary.Rank = int.Parse(teamRank.Replace("#",""));
            }
            var teamWeeksInTop30Core = int.Parse(profileTeamStats[1].FindElement(By.TagName("span")).Text.Trim());
            team.Summary.WeeksInTop30Core = teamWeeksInTop30Core;

            var coach = profileTeamStats.Find(stat => stat.FindElement(By.TagName("b")).Text == "Coach");
            var teamCoach = coach.FindElement(By.CssSelector("a.a-reset.right")).Text.Trim().Split("'")[1].Replace("'", string.Empty);
            team.Summary.Coach.NickName = teamCoach != null ? teamCoach : "";

            var socials = Driver.FindElement(By.CssSelector("div.socialMediaButtons")).FindElements(By.TagName("a")).ToList();
            socials.ForEach(social => team.Summary.Socials.Add((social.GetAttribute("href").Split(".")[1], social.GetAttribute("href"))));
        }

        private void ScrapeRoster(Team team)
        {
            team.Summary.Coach = new Coach();
            var coachTable = Driver.FindElement(By.CssSelector("table.table-container.coach-table"));
            var coachName = coachTable.FindElement(By.CssSelector("div.text-ellipsis")).Text;
            team.Summary.Coach.NickName = coachName;
            var coachCountry = coachTable.FindElement(By.CssSelector("img.flag")).GetAttribute("alt");
            team.Summary.Coach.Country = coachCountry;
            var centerCoachCols = Driver.FindElements(By.CssSelector("div.players-cell.center-cell.opacity-cell"));
            if (centerCoachCols.ToList().Count == 3)
            {
                string timeOnTeam = centerCoachCols[0].Text;
                team.Summary.Coach.TimeOnTeam = timeOnTeam;
                string mapsCoached = centerCoachCols[1].Text;
                team.Summary.Coach.MapsCoached = int.Parse(mapsCoached);
                string trophies = centerCoachCols[2].Text;
                team.Summary.Coach.Trophies = int.Parse(trophies);
            }
            var coachWinrate = coachTable.FindElement(By.CssSelector("div.players-cell.rating-cell")).Text;
            team.Summary.Coach.Winrate = double.Parse(coachWinrate.Replace("%", ""));

            var playerRows = Driver.FindElements(By.CssSelector("table.table-container.players-table > tbody > tr"));
            foreach (var row in playerRows)
            {
                var playerCols = row.FindElements(By.TagName("td"));
                Debug.WriteLine(playerCols.ToArray().Length);
                var player = new Player();
                var nickName = playerCols[0].FindElement(By.CssSelector("div.text-ellipsis")).Text;
                player.NickName = nickName;
                var country = playerCols[0].FindElement(By.CssSelector("img.flag")).GetAttribute("alt");
                player.Country = country;
                var status = playerCols[1].FindElement(By.CssSelector("div.players-cell.status-cell > div")).Text;
                player.Status = status;
                var timeOnTeamString = playerCols[2].FindElement(By.TagName("div")).Text;
                DateTime timeOnTeam = ParseDurationString(timeOnTeamString);
                player.TimeOnTeam = timeOnTeam;
                var playerMapsPlayed = playerCols[3].FindElement(By.TagName("div")).Text;
                player.MapsPlayed = int.Parse(playerMapsPlayed);
                var playerRating = playerCols[4].FindElement(By.TagName("div")).Text;
                player.Rating = double.Parse(playerRating);
                team.Roster.Add(player);
            }
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
        private DateTime ParseDurationString(string duration)
        {
            int years = 0, months = 0;
            var yearMatch = Regex.Match(duration, @"(\d+)\s+year");
            var monthMatch = Regex.Match(duration, @"(\d+)\s+month");

            if (yearMatch.Success)
            {
                years = int.Parse(yearMatch.Groups[1].Value);
            }

            if (monthMatch.Success)
            {
                months = int.Parse(monthMatch.Groups[1].Value);
            }

            if (!yearMatch.Success && !monthMatch.Success)
            {
                throw new ArgumentException("Invalid duration string format in ParseDurationString.");
            }

            DateTime currentDate = DateTime.Now;
            DateTime targetDate = currentDate.AddYears(-years).AddMonths(-months);
            return targetDate;
        }
    }
}
