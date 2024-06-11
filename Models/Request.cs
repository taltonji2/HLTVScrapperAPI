namespace HLTVScrapperAPI.Models
{
    public class ScrapeRequest {
        public bool IsTimeFrameValid(string timeFrame)
        {
            List<string> validTimeFrames = new List<string>
            {
                "All time",
                "Last month",
                "Last 3 months",
                "Last 6 months",
                "Last 12 months",
                "2024",
                "2023",
                "2022",
                "2021",
                "2020",
                "2019",
                "2018",
                "2017",
                "2016",
                "2015",
                "2014",
                "2013",
                "2012"
            };

            if (validTimeFrames.Contains(timeFrame) || timeFrame == "")
            {
                return true;
            }

            return false;
        }
    }


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
