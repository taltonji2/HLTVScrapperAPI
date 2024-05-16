namespace HLTVScrapperAPI.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Player
    {
        public Player () { }
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
        public OverallStats Overall { get; set; }
        public RoundStats Round { get; set; }
        public OpeningStats Opening { get; set; }
        public WeaponStats Weapon { get; set; }
        public RatingStats Rating { get; set; }
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

    public class OverallStats
    {
        public string Kills { get; set; }
        public string Deaths { get; set; }
        public string KillDeath { get; set; }
        public string KillRound { get; set; }
        public string RoundsWithKills { get; set; }
    }

    public class RoundStats
    {
        public string TotalOpeningKills { get; set; }
        public string TotalOpeningDeaths { get; set; }
        public string OpeningKillRatio { get; set; }
        public string OpeningKillRating { get; set; }
        public string TeamWinPercentAfterFirstKill { get; set; }
        public string FirstKillWonRounds { get; set; }
    }

    public class OpeningStats
    {
        public string NoKillRounds { get; set; }
        public string SingleKillRounds { get; set; }
        public string DoubleKillRounds { get; set; }
        public string TripleKillRounds { get; set; }
        public string QuadKillRounds { get; set; }
        public string AceRounds { get; set; }
    }

    public class WeaponStats
    {
        public string RifleKills { get; set; }
        public string SniperKills { get; set; }
        public string SMGKills { get; set; }
        public string PistolKills { get; set; }
        public string Grenade { get; set; }
        public string Other { get; set; }
    }

    public class RatingStats
    {
        public string Rating2_0Top5 { get; set; }
        public string Rating2_0Top10 { get; set; }
        public string Rating2_0Top20 { get; set; }
        public string Rating2_0Top30 { get; set; }
        public string Rating2_0Top50 { get; set; }
    }

    public static class PlayerMapper
    {
        public static Player MapJsonToPlayer(string json)
        {
            JObject jsonObject = JObject.Parse(json);

            Player player = new Player();

            player.NickName = MapJsonProperty(jsonObject, "nickName");
            player.County = MapJsonProperty(jsonObject, "county");
            player.FullName = MapJsonProperty(jsonObject, "fullName");
            player.TeamName = MapJsonProperty(jsonObject, "teamName");
            player.Age = MapJsonProperty(jsonObject, "age");
            player.Rating1_0 = MapJsonProperty(jsonObject, "RATING 1.0");
            player.DPR = MapJsonProperty(jsonObject, "DPR");
            player.KAST = MapJsonProperty(jsonObject, "KAST");
            player.Impact = MapJsonProperty(jsonObject, "IMPACT");
            player.ADR = MapJsonProperty(jsonObject, "ADR");
            player.KPR = MapJsonProperty(jsonObject, "KPR");

            // Map nested properties
            player.General = MapGeneralStats(jsonObject["general"]);
            player.Overall = MapOverallStats(jsonObject["overall"]);
            player.Round = MapRoundStats(jsonObject["round"]);
            player.Opening = MapOpeningStats(jsonObject["opening"]);
            player.Weapon = MapWeaponStats(jsonObject["weapon"]);
            player.Rating = MapRatingStats(jsonObject["rating"]);

            return player;
        }

        private static string MapJsonProperty(JObject jsonObject, string key)
        {
            JToken token = jsonObject[key];

            return token?.Value<string>();
        }

        private static GeneralStats MapGeneralStats(JToken token)
        {
            if (token == null)
                return null;

            var generalStats = new GeneralStats();

            Dictionary<string, string> propertyMap = new Dictionary<string, string>
            {
                { "Total kills", "TotalKills" },
                { "Headshot %", "HeadshotPercentage" },
                { "Total deaths", "TotalDeaths" },
                { "K/D Ratio", "KD" },
                { "Damage / Round", "DmgPerRound" },
                { "Grenade dmg / Round", "GrenadeDmgPerRound" },
                { "Maps played", "MapsPlayed" },
                { "Rounds played", "RoundsPlayed" },
                { "Kills / round", "KillsPerRound" },
                { "Assists / round", "AssistsPerRound" },
                { "Deaths / round", "DeathsPerRound" },
                { "Saved by teammate / round", "SavedPerRound" },
                { "Saved teammates / round", "SavesPerRound" },
                { "Rating 1.0", "Rating1_0" }
            };

            foreach (var pair in propertyMap)
            {
                if (token[pair.Key] != null)
                {
                    typeof(GeneralStats).GetProperty(pair.Value)?.SetValue(generalStats, token[pair.Key].Value<string>());
                }
            }

            return generalStats;
        }

        private static OverallStats MapOverallStats(JToken token)
        {
            if (token == null)
                return null;

            var overallStats = new OverallStats();

            Dictionary<string, string> propertyMap = new Dictionary<string, string>
            {
                { "Kills", "Kills" },
                { "Deaths", "Deaths" },
                { "Kill / Death", "KillDeath" },
                { "Kill / Round", "KillRound" },
                { "Rounds with kills", "RoundsWithKills" }
            };

            foreach (var pair in propertyMap)
            {
                if (token[pair.Key] != null)
                {
                    typeof(OverallStats).GetProperty(pair.Value)?.SetValue(overallStats, token[pair.Key].Value<string>());
                }
            }

            return overallStats;
        }

        private static RoundStats MapRoundStats(JToken token)
        {
            if (token == null)
                return null;

            var roundStats = new RoundStats();

            Dictionary<string, string> propertyMap = new Dictionary<string, string>
            {
                { "Total opening kills", "TotalOpeningKills" },
                { "Total opening deaths", "TotalOpeningDeaths" },
                { "Opening kill ratio", "OpeningKillRatio" },
                { "Opening kill rating", "OpeningKillRating" },
                { "Team win percent after first kill", "TeamWinPercentAfterFirstKill" },
                { "First kill in won rounds", "FirstKillWonRounds" }
            };

            foreach (var pair in propertyMap)
            {
                if (token[pair.Key] != null)
                {
                    typeof(RoundStats).GetProperty(pair.Value)?.SetValue(roundStats, token[pair.Key].Value<string>());
                }
            }

            return roundStats;
        }

        private static OpeningStats MapOpeningStats(JToken token)
        {
            if (token == null)
                return null;

            var openingStats = new OpeningStats();

            Dictionary<string, string> propertyMap = new Dictionary<string, string>
            {
                { "0 kill rounds", "NoKillRounds" },
                { "1 kill rounds", "SingleKillRounds" },
                { "2 kill rounds", "DoubleKillRounds" },
                { "3 kill rounds", "TripleKillRounds" },
                { "4 kill rounds", "QuadKillRounds" },
                { "5 kill rounds", "AceRounds" }
            };

            foreach (var pair in propertyMap)
            {
                if (token[pair.Key] != null)
                {
                    typeof(OpeningStats).GetProperty(pair.Value)?.SetValue(openingStats, token[pair.Key].Value<string>());
                }
            }

            return openingStats;
        }

        private static WeaponStats MapWeaponStats(JToken token)
        {
            if (token == null)
                return null;

            var weaponStats = new WeaponStats();

            Dictionary<string, string> propertyMap = new Dictionary<string, string>
            {
                { "Rifle kills", "RifleKills" },
                { "Sniper kills", "SniperKills" },
                { "SMG kills", "SMGKills" },
                { "Pistol kills", "PistolKills" },
                { "Grenade", "Grenade" },
                { "Other", "Other" }
            };

            foreach (var pair in propertyMap)
            {
                if (token[pair.Key] != null)
                {
                    typeof(WeaponStats).GetProperty(pair.Value)?.SetValue(weaponStats, token[pair.Key].Value<string>());
                }
            }

            return weaponStats;
        }

        private static RatingStats MapRatingStats(JToken token)
        {
            if (token == null)
                return null;

            var ratingStats = new RatingStats();

            Dictionary<string, string> propertyMap = new Dictionary<string, string>
            {
                { "RATING 2.0 vs top 5 opponents", "Rating2_0Top5" },
                { "RATING 2.0 vs top 10 opponents", "Rating2_0Top10" },
                { "RATING 2.0 vs top 20 opponents", "Rating2_0Top20" },
                { "RATING 2.0 vs top 30 opponents", "Rating2_0Top30" },
                { "RATING 2.0 vs top 50 opponents", "Rating2_0Top50" }
            };

            foreach (var pair in propertyMap)
            {
                if (token[pair.Key] != null)
                {
                    typeof(RatingStats).GetProperty(pair.Value)?.SetValue(ratingStats, token[pair.Key].Value<string>());
                }
            }

            return ratingStats;
        }
    }

    }
