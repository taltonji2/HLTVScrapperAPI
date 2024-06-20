namespace HLTVScrapperAPI.Models
{
    public class Player
    {
        public Player()
        {
            Summary = new PlayerSummary
            {
                NickName = "",
                County = "",
                FullName = "",
                CurrentTeam = null,
                Age = null,
                Status = null,
                TimeOnTeam = null,
                MapsPlayed = null,
                RatingCurrentTeamPeriod = null,
                Socials = new List<(string, string)>()
            };

            Statistics = new PlayerStatistics
            {
                Rating2 = null,
                KillsPerRound = null,
                Headshots = null,
                MapsPlayed = null,
                DeathsPerRound = null,
                RoundsContributed = null
            };
            TeamStatistics = new PlayerTeamStats
            {
                DaysInCurrentTeam = 0,
                DaysInTeams = 0,
                Teams = 0,
                TeamsBreakdown = new List<PlayerTeamStats.TeamBreakdown>()
            };

            Achievements = new PlayerAchievements
            {
                MajorsWon = 0,
                MajorsPlayed = 0,
                LANsWon = 0,
                LANsPlayed = 0,
                LANAchievements = new List<PlayerEventAchievement>(),
                MajorAchievements = new List<PlayerEventAchievement>(),
                Top20Achievements = new List<PlayerAchievements.Top20Rank>()
            };
        }
        public PlayerSummary Summary { get; set; }
        public PlayerStatistics Statistics { get; set; }
        public PlayerTeamStats TeamStatistics { get; set; }
        public PlayerAchievements Achievements { get; set; }
    }
    public class PlayerSummary
    {
        public string NickName { get; set; }
        public string County { get; set; }
        public string FullName { get; set; }
        public string? CurrentTeam { get; set; }
        public int? Age { get; set; }
        public string? Status { get; set; }
        public string? TimeOnTeam { get; set; }
        public int? MapsPlayed { get; set; }
        public double? RatingCurrentTeamPeriod { get; set; }
        public List<(string, string)> Socials { get; set; }
    }
    public class PlayerStatistics
    {
        public double? Rating2 { get; set; }
        public double? KillsPerRound { get; set; }
        public double? Headshots { get; set; }
        public int? MapsPlayed { get; set; }
        public double? DeathsPerRound { get; set; }
        public double? RoundsContributed { get; set; }
    }
    public class PlayerTeamStats
    {
        public class TeamBreakdown { public string TeamName; public DateTime Start; public DateTime? End; }
        public int DaysInCurrentTeam { get; set; }
        public int DaysInTeams { get; set; }
        public int Teams { get; set; }
        public List<TeamBreakdown> TeamsBreakdown { get; set; }
    }
    public class PlayerAchievements
    {
        public class Top20Rank { public int year; public int rank; }
        public int MajorsWon { get; set; }
        public int MajorsPlayed { get; set; }
        public int LANsWon { get; set; }
        public int LANsPlayed { get; set; }
        public List<PlayerEventAchievement> LANAchievements { get; set; }
        public List<PlayerEventAchievement> MajorAchievements { get; set; }
        public List<Top20Rank> Top20Achievements { get; set; }

    }
    public class PlayerEventAchievement
    {
        public string Placement { get; set; }
        public string Tournament { get; set; }
        public string Team { get; set; }
    }


}
