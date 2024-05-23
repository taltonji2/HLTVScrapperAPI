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

    public class TeamScrapeRequest : ScrapeRequest
    {
        public string TeamName { get; set; }
        public string Filter { get; set; }
    }

    public class MatchScrapeRequest : ScrapeRequest 
    {
        public string MatchName { get; set; }
    }

    public class EventScrapeRequest : ScrapeRequest 
    { 
    
    }


}
