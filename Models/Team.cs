namespace HLTVScrapperAPI.Models.Team
{
    public class Team
    {
        public Team ()
        {
            Summary = new TeamSummary {
                Name = "",
                Coach = new Coach
                {
                    NickName = null,
                    Country = null,
                    TimeOnTeam = null,
                    MapsCoached = null,
                    Trophies = null,
                    Winrate = null
                },
                WeeksInTop30Core = 0,
                Country = "",
                Rank = null,
                Socials = new List<(string, string)>()
            };
            Roster = new List<Player> ();
            RecentMatches = new List<Match> ();
            UpcomingMatches = new List<Match> ();
            UpcomingEvents = new List<Event> ();
        }
        public TeamSummary Summary { get; set; }
        public List<Player> Roster { get; set; }
        public List<Match> RecentMatches { get; set; }
        public List<Match> UpcomingMatches { get; set; }
        public List<Event> UpcomingEvents { get; set; }
        public int MapsPlayed { get; set; }
    }

    public class TeamSummary
    {
        public string Name { get; set; }
        public Coach Coach { get; set; }
        public string Country { get; set; }
        public int? Rank { get; set; }
        public int WeeksInTop30Core { get; set; }
        public double AveragePlayerAge { get; set; }
        public List<(string, string)>? Socials { get; set; }
    }

    public class Coach
    {
        public string? NickName { get; set; }
        public string? Country { get; set; }
        public string? TimeOnTeam { get; set; }
        public int? MapsCoached { get; set; }
        public int? Trophies { get; set; }
        public double? Winrate { get; set; }
    }
    public class Player
    {
        public string NickName { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public DateTime TimeOnTeam { get; set; }
        public int MapsPlayed { get; set; }
        public double Rating { get; set; }
    }

    public class TeamStatistics
    {
        public List<string> LastFiveMatches { get; set; }
    }

    public class Map
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public double RoundWinrateAfterGettingFirstKill { get; set; }
        public double RoundWinrateAfterFirstDeath { get; set; }
        public double PistolRoundWinrate { get; set; }
    }

    public class Event
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string PrizePool { get; set; }
        public List<string> Teams { get; set; }
    }
}
