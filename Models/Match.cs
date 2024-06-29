namespace HLTVScrapperAPI.Models
{
    public class Match
    {
        public Match() { }
        public string Event { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
    }
}
