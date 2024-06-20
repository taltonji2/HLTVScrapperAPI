namespace HLTVScrapperAPI.Models
{
    public class Team
    {
        public Team ()
        {
            Summary = new TeamSummary ();
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
        public List<Player> Players { get; set; }
        public int MapsPlayed { get; set; }
    }

    public class TeamSummary
    {
        public string Name { get; set; }
        public Coach Coach { get; set; }
        public string Country { get; set; }
        public int Rank { get; set; }
        public int WeeksInTop30Core { get; set; }
        public double AveragePlayerAge { get; set; }
        public List<(string, string)> Socials { get; set; }
    }

    public class Coach
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string TimeOnTeam { get; set; }
        public int MapsCoached { get; set; }
        public int Trophies { get; set; }
        public double Winrate { get; set; }
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
