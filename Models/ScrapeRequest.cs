using System.Runtime.Serialization;

namespace HLTVScrapperAPI.Models.ScrapeRequest
{
    public class ScrapeRequest
    {
        private enum FilterType
        {
            [EnumMember(Value = "AllTime")]
            AllTime,
            [EnumMember(Value = "LastMonth")]
            LastMonth,
            [EnumMember(Value = "LastThreeMonths")]
            LastThreeMonths,
            [EnumMember(Value = "LastSixMonths")]
            LastSixMonths,
            [EnumMember(Value = "LastTwelveMonths")]
            LastTwelveMonths,
            [EnumMember(Value = "Year")]
            Year,
        }
    }

    public class PlayerScrapeRequest
    {
        public string PlayerName { get; set; }
        public string Filter { get; set; }
    }
}
