using System.Collections.Generic;

namespace HLTVScrapperAPI.Models
{
    public class ScrapeResult<T>
    {
        public ScrapeResult(T scrapeObject)
        {
            Success = true;
            Errors = new List<string>();
            ScrapeObject = scrapeObject;
        }

        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public T ScrapeObject { get; set; }

    }
}
