namespace HLTVScrapperAPI.Models
{
    public class ScrapeRequest { }

    public class PlayerScrapeRequest : ScrapeRequest
    {
        public string Name { get; set; }
        public string TimeFrame { get; set; }
    }

    public class TeamScrapeRequest : ScrapeRequest
    {
        public string Name { get; set; }
        public string TimeFrame { get; set; }
    }

    public class MatchScrapeRequest : ScrapeRequest 
    {
        public string TeamOne { get; set; }
        public string TeamTwo { get; set; }
    }

    public class EventScrapeRequest : ScrapeRequest { }


}
