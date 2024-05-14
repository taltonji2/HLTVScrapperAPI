namespace HLTVScrapperAPI.Models
{
    public class ScrapeRequest
    {

    }

    public class PlayerScrapeRequest : ScrapeRequest
    {
        public string PlayerName { get; set; }
        public string Filter { get; set; }
    }
}
