using OpenQA.Selenium;
using HLTVScrapperAPI.Models;
using System.Diagnostics;
using HLTVScrapperAPI.Models.Player;

namespace HLTVScrapperAPI.Services
{
    public class PlayerScraper : Scraper
    {
        public PlayerScraper() : base() {}

        public ScrapeResult<Player> ScrapePlayer(string name)
        {
            Player player = new Player();
            ScrapeResult<Player> result = new ScrapeResult<Player>(player);
            try
            {
                this.SearchScrapeObject("player", name);
                string playerUrl = Driver.Url.Replace("#tab-infoBox", "");
                this.ScrapeSummaryInfo(player);
                this.ScrapePlayerStatistics(player);

                //Additions
                Driver.Navigate().GoToUrl(playerUrl + "#tab-teamsBox");
                this.ScrapePlayerTeamStats(player);
                Driver.Navigate().GoToUrl(playerUrl + "#tab-achievementBox");
                this.ScrapePlayerAchievements(player);
                Driver.Navigate().GoToUrl(playerUrl + "#tab-trophiesBox");
                this.ScrapePlayerTrophies(player);
            }
            catch (InvalidOperationException e)
            {
                Debug.WriteLine(e.Message);
                result.Errors.Add(e.Message);
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine(e.Message);
                result.Errors.Add(e.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                result.Errors.Add(e.Message);
            }
            finally
            {
                this.DisposeDriver();
                if (result.Errors.Count > 0) { result.Success = false; }
            }
            return result;
        }
        private void ScrapeSummaryInfo (Player player)
        {
            var nickName = Driver.FindElement(By.CssSelector("h1.playerNickname")).Text;
            player.Summary.NickName = nickName != null ? nickName : "";

            var realName = Driver.FindElement(By.CssSelector("div.playerRealname")).Text;
            player.Summary.FullName = realName != null ? realName : "";

            var country = Driver.FindElement(By.CssSelector("div.playerRealname"))
                .FindElement(By.CssSelector("img.flag")).GetAttribute("alt");
            player.Summary.County = country != null ? country : "";

            var age = Driver.FindElement(By.CssSelector("div.playerSummaryRow.playerAge"))
                .FindElement(By.CssSelector("span.listRight"))
                .FindElement(By.TagName("span")).Text;
            player.Summary.Age = age != null ? int.Parse(age.Split(" ")[0]) : 0;

            var team = Driver.FindElement(By.CssSelector("span.listRight.text-ellipsis")).FindElement(By.TagName("a")).Text;
            player.Summary.CurrentTeam = team != null ? team : "";

            var socials = Driver.FindElement(By.CssSelector("div.socialMediaButtons")).FindElements(By.TagName("a")).ToList();
            socials.ForEach(social => player.Summary.Socials.Add((social.GetAttribute("href").Split(".")[1], social.GetAttribute("href"))));
        }
        private void ScrapePlayerStatistics(Player player)
        {
            var statisticsContainer = Driver.FindElement(By.CssSelector("div.playerpage-container"));

            var statistics = statisticsContainer.FindElements(By.CssSelector("div.player-stat"));

            var rating2 = statistics[0].FindElement(By.CssSelector("span.statsVal")).Text;
            player.Statistics.Rating2 = rating2 != null ? Double.Parse(rating2) : 0;

            var killsPerRound = statistics[1].FindElement(By.CssSelector("span.statsVal")).Text;
            player.Statistics.KillsPerRound = killsPerRound != null ? Double.Parse(killsPerRound) : 0;

            var headshots = statistics[2].FindElement(By.CssSelector("span.statsVal")).Text;
            player.Statistics.Headshots = headshots != null ? Double.Parse(headshots) : 0;

            var mapsPlayed = statistics[3].FindElement(By.CssSelector("span.statsVal")).Text;
            player.Statistics.MapsPlayed = mapsPlayed != null ? int.Parse(mapsPlayed) : 0;

            var deathsPerRound = statistics[4].FindElement(By.CssSelector("span.statsVal")).Text;
            player.Statistics.DeathsPerRound = deathsPerRound != null ? Double.Parse(deathsPerRound) : 0;

            var roundsContributed = statistics[5].FindElement(By.CssSelector("span.statsVal")).Text;
            player.Statistics.RoundsContributed = roundsContributed != null ? Double.Parse(roundsContributed) : 0;
        }
        private void ScrapePlayerTeamStats(Player player)
        {
            var teamStats = Driver.FindElements(By.CssSelector("div.highlighted-stat"));
            foreach (var item in teamStats)
            {
                string statisticname = item.FindElement(By.CssSelector("div.description")).Text;
                string statisticvalue = item.FindElement(By.CssSelector("div.stat")).Text;
                switch (statisticname)
                {
                    case "Days in teams":
                        player.TeamStatistics.DaysInTeams = int.Parse(statisticvalue);
                        break;
                    case "Days in current team":
                        player.TeamStatistics.DaysInCurrentTeam = int.Parse(statisticvalue);
                        break;
                    case "Teams":
                        player.TeamStatistics.Teams = int.Parse(statisticvalue);
                        break;
                }
            }
            var teamBreakdowns = Driver.FindElements(By.CssSelector("tr.team"));
            foreach (var item in teamBreakdowns)
            {
                var teamName = item.FindElement(By.CssSelector("td.teamCol")).Text;
                var spans = item.FindElements(By.TagName("span")).Where(span => span.GetAttribute("data-time-format") == "MM yyyy").ToList();
                
                if (spans.Count() == 2)
                {
                    string start = spans[0].Text;
                    string startMonth = start.Split(" ")[0];
                    string startYear = start.Split(" ")[1];
                    string end = spans[1].Text;
                    string endMonth = end.Split(" ")[0];
                    string endYear = end.Split(" ")[1];

                    TeamStats.TeamBreakdown team = new TeamStats.TeamBreakdown
                    {
                        TeamName = teamName,
                        Start = new DateTime(int.Parse(startYear), int.Parse(startMonth), 1),
                        End = null,
                    };

                    if ( end != "Present")
                    {
                        team.End = new DateTime(int.Parse(endYear), int.Parse(endMonth), 1);
                    }

                    player.TeamStatistics.TeamsBreakdown.Add(team);
                }
            }
        }
        private void ScrapePlayerAchievements(Player player)
        {
            var majorAchievementStats = Driver.FindElements(By.CssSelector("div.highlighted-stat"));
            foreach (var item in majorAchievementStats)
            {
                string statisticname = item.FindElement(By.CssSelector("div.description")).Text;
                string statisticvalue = item.FindElement(By.CssSelector("div.stat")).Text;
                switch (statisticname)
                {
                    case "Majors won":
                        player.Achievements.MajorsWon = int.Parse(statisticvalue);
                        break;
                    case "Majors played":
                        player.Achievements.MajorsPlayed = int.Parse(statisticvalue);
                        break;
                }
            }
            
            var majorAchievementBreakdowns = Driver.FindElements(By.CssSelector("tr.team"));
            foreach (var item in majorAchievementBreakdowns)
            {
                var placement = item.FindElement(By.CssSelector("td.placement-cell")).FindElement(By.CssSelector("div.achievement")).Text;
                var team = item.FindElement(By.CssSelector("td.team-name-cell")).FindElement(By.CssSelector("span.team-name")).Text;
                var tournament = item.FindElement(By.CssSelector("td.tournament-name-cell")).FindElement(By.TagName("a")).Text;

                EventAchievement majorAchievement = new EventAchievement
                {
                    Placement = placement,
                    Team = team,
                    Tournament = tournament,
                };

                player.Achievements.MajorAchievements.Add(majorAchievement);
            }

            Driver.FindElements(By.CssSelector("div.sub-navigation-link.sub-tab"))
                .First(element => element.GetAttribute("data-content-id") == "lanAchievement").Click();

            var lanAchievementStats = Driver.FindElements(By.CssSelector("div.highlighted-stat"));
            foreach (var item in lanAchievementStats)
            {
                string statisticname = item.FindElement(By.CssSelector("div.description")).Text;
                string statisticvalue = item.FindElement(By.CssSelector("div.stat")).Text;
                switch (statisticname)
                {
                    case "LANs won":
                        player.Achievements.LANsWon = int.Parse(statisticvalue);
                        break;
                    case "LANs played":
                        player.Achievements.LANsPlayed = int.Parse(statisticvalue);
                        break;
                }
            }

            var lanAchievementBreakdowns = Driver.FindElements(By.CssSelector("tr.team"));
            foreach (var item in lanAchievementBreakdowns)
            {
                var placement = item.FindElement(By.CssSelector("td.placement-cell")).FindElement(By.CssSelector("div.achievement")).Text;
                var team = item.FindElement(By.CssSelector("td.team-name-cell")).FindElement(By.CssSelector("span.team-name")).Text;
                var tournament = item.FindElement(By.CssSelector("td.tournament-name-cell")).FindElement(By.TagName("a")).Text;

                EventAchievement lanAchievement = new EventAchievement
                {
                    Placement = placement,
                    Team = team,
                    Tournament = tournament,
                };

                player.Achievements.LANAchievements.Add(lanAchievement);
            }
        }
        private void ScrapePlayerTrophies(Player player)
        {
            var trophies = Driver.FindElements(By.CssSelector("tr.trophy-row"));
            foreach (var item in trophies)
            {
                var year = item.FindElement(By.CssSelector("div.trophy-rating-number")).Text;
                var rank = item.FindElement(By.CssSelector("div.trophy-event")).FindElement(By.TagName("a")).Text;
                rank = rank.Split(" ")[0].Replace("#", "");

                Achievements.Top20Rank top20Rank = new Achievements.Top20Rank
                {
                    year = int.Parse(year),
                    rank = int.Parse(rank),
                };

                player.Achievements.Top20Achievements.Add(top20Rank);
            }
        }
    }
}
