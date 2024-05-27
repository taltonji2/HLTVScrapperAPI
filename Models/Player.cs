namespace HLTVScrapperAPI.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Player
    {
        public Player()
        {
            general = new GeneralStats
            {
                totalKills = "",
                headshotPercentage = "",
                totalDeaths = "",
                kD = "",
                dmgPerRound = "",
                grenadeDmgPerRound = "",
                mapsPlayed = "",
                roundsPlayed = "",
                killsPerRound = "",
                assistsPerRound = "",
                deathsPerRound = "",
                savedPerRound = "",
                savesPerRound = "",
                rating1_0 = ""
            };

            opponentRating = new OpponentRatingStats
            {
                ratingTop5 = new OpponentRatingDetails(),
                ratingTop10 = new OpponentRatingDetails(),
                ratingTop20 = new OpponentRatingDetails(),
                ratingTop30 = new OpponentRatingDetails(),
                ratingTop50 = new OpponentRatingDetails()
            };
        }
        public string nickName { get; set; }
        public string county { get; set; }
        public string fullName { get; set; }
        public string teamName { get; set; }
        public string age { get; set; }
        public string rating1_0 { get; set; }
        public string DPR { get; set; }
        public string KAST { get; set; }
        public string impact { get; set; }
        public string ADR { get; set; }
        public string KPR { get; set; }
        public GeneralStats general { get; set; }
        public OpponentRatingStats opponentRating { get; set; }
    }

    public class GeneralStats
    {
        public string totalKills { get; set; }
        public string headshotPercentage { get; set; }
        public string totalDeaths { get; set; }
        public string kD { get; set; }
        public string dmgPerRound { get; set; }
        public string grenadeDmgPerRound { get; set; }
        public string mapsPlayed { get; set; }
        public string roundsPlayed { get; set; }
        public string killsPerRound { get; set; }
        public string assistsPerRound { get; set; }
        public string deathsPerRound { get; set; }
        public string savedPerRound { get; set; }
        public string savesPerRound { get; set; }
        public string rating1_0 { get; set; }
    }

    public class OpponentRatingStats
    {
        public OpponentRatingDetails ratingTop5 { get; set; }
        public OpponentRatingDetails ratingTop10 { get; set; }
        public OpponentRatingDetails ratingTop20 { get; set; }
        public OpponentRatingDetails ratingTop30 { get; set; }
        public OpponentRatingDetails ratingTop50 { get; set; }
    }

    public class OpponentRatingDetails
    {
        public string rating { get; set; }

        public string maps { get; set; }
    }


}
