namespace HLTVScrapperAPI.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Player
    {
        public Player()
        {
            General = new GeneralStats
            {
                TotalKills = "",
                HeadshotPercentage = "",
                TotalDeaths = "",
                KD = "",
                DmgPerRound = "",
                GrenadeDmgPerRound = "",
                MapsPlayed = "",
                RoundsPlayed = "",
                KillsPerRound = "",
                AssistsPerRound = "",
                DeathsPerRound = "",
                SavedPerRound = "",
                SavesPerRound = "",
                Rating1_0 = ""
            };

            OpponentRating = new OpponentRatingStats
            {
                RatingTop5 = new OpponentRatingDetails(),
                RatingTop10 = new OpponentRatingDetails(),
                RatingTop20 = new OpponentRatingDetails(),
                RatingTop30 = new OpponentRatingDetails(),
                RatingTop50 = new OpponentRatingDetails()
            };
        }
        public string NickName { get; set; }
        public string County { get; set; }
        public string FullName { get; set; }
        public string TeamName { get; set; }
        public string Age { get; set; }
        public string Rating1_0 { get; set; }
        public string DPR { get; set; }
        public string KAST { get; set; }
        public string Impact { get; set; }
        public string ADR { get; set; }
        public string KPR { get; set; }
        public GeneralStats General { get; set; }
        public OpponentRatingStats OpponentRating { get; set; }
    }

    public class GeneralStats
    {
        public string TotalKills { get; set; }
        public string HeadshotPercentage { get; set; }
        public string TotalDeaths { get; set; }
        public string KD { get; set; }
        public string DmgPerRound { get; set; }
        public string GrenadeDmgPerRound { get; set; }
        public string MapsPlayed { get; set; }
        public string RoundsPlayed { get; set; }
        public string KillsPerRound { get; set; }
        public string AssistsPerRound { get; set; }
        public string DeathsPerRound { get; set; }
        public string SavedPerRound { get; set; }
        public string SavesPerRound { get; set; }
        public string Rating1_0 { get; set; }
    }

    public class OpponentRatingStats
    {
        public OpponentRatingDetails RatingTop5 { get; set; }
        public OpponentRatingDetails RatingTop10 { get; set; }
        public OpponentRatingDetails RatingTop20 { get; set; }
        public OpponentRatingDetails RatingTop30 { get; set; }
        public OpponentRatingDetails RatingTop50 { get; set; }
    }

    public class OpponentRatingDetails
    {
        public string rating { get; set; }
        public string maps { get; set; }
    }


}
