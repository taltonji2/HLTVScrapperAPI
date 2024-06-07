namespace HLTVScrapperAPI.Models
{
    public class Team
    {
        public Team ()
        {
            Roster = new List<TeamPlayer>();

            Ancient = new Map
            {
                WinRate = "",
                RoundsPlayed = "",
                RoundWinRateAfterFirstKill = "",
                RoundWinRateAfterFirstDeath = "",
                BiggestWin = "",
                BiggestLoss = ""
            };
            Anubis = new Map
            {
                WinRate = "",
                RoundsPlayed = "",
                RoundWinRateAfterFirstKill = "",
                RoundWinRateAfterFirstDeath = "",
                BiggestWin = "",
                BiggestLoss = ""
            };
            Inferno = new Map
            {
                WinRate = "",
                RoundsPlayed = "",
                RoundWinRateAfterFirstKill = "",
                RoundWinRateAfterFirstDeath = "",
                BiggestWin = "",
                BiggestLoss = ""
            };
            Mirage = new Map
            {
                WinRate = "",
                RoundsPlayed = "",
                RoundWinRateAfterFirstKill = "",
                RoundWinRateAfterFirstDeath = "",
                BiggestWin = "",
                BiggestLoss = ""
            };
            Nuke = new Map
            {
                WinRate = "",
                RoundsPlayed = "",
                RoundWinRateAfterFirstKill = "",
                RoundWinRateAfterFirstDeath = "",
                BiggestWin = "",
                BiggestLoss = ""
            };
            Overpass = new Map
            {
                WinRate = "",
                RoundsPlayed = "",
                RoundWinRateAfterFirstKill = "",
                RoundWinRateAfterFirstDeath = "",
                BiggestWin = "",
                BiggestLoss = ""
            };
            Vertigo = new Map
            {
                WinRate = "",
                RoundsPlayed = "",
                RoundWinRateAfterFirstKill = "",
                RoundWinRateAfterFirstDeath = "",
                BiggestWin = "",
                BiggestLoss = ""
            };
        }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Rank { get; set; }
        public List<TeamPlayer> Roster { get; set; }
        public string Coach { get; set; }
        public string MapsPlayed { get; set; }
        public string WinRate { get; set; }
        public string RoundsPlayed { get; set; }
        public string KD { get; set; }
        public Map Anubis { get; set; }
        public Map Ancient { get; set; }
        public Map Inferno { get; set; }
        public Map Mirage { get; set; }
        public Map Nuke { get; set; }
        public Map Overpass { get; set; }
        public Map Vertigo { get; set; }
    }

    public class Map
    {
        public string WinRate { get; set; }
        public string RoundsPlayed { get; set; }
        public string RoundWinRateAfterFirstKill { get; set; }
        public string RoundWinRateAfterFirstDeath { get; set; }
        public string BiggestWin { get; set; }
        public string BiggestLoss { get; set; }
    }

    public class TeamPlayer
    {
        public TeamPlayer(string name = "", string rating = "", string maps = "")
        {
            this.name = name;
            this.rating = rating;
            this.maps = maps;
        }
        public string name { get; set; }
        public string rating { get; set; }
        public string maps { get; set; }
    }
}
