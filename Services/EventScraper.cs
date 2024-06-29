using OpenQA.Selenium;
using HLTVScrapperAPI.Models;

namespace HLTVScrapperAPI.Services
{
    public class EventScraper : Scraper
    {
        public EventScraper() : base() {}

        public ScrapeResult<BigEvent> ScrapeBigEvents ()
        {
            BigEvent bigevent = new BigEvent();
            ScrapeResult<BigEvent> result = new ScrapeResult<BigEvent>(bigevent);
            try
            {
                
            }
            catch (InvalidOperationException e)
            {
                result.Errors.Add(e.Message);
            }
            catch (NoSuchElementException e)
            {
                result.Errors.Add(e.Message);
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
            }
            finally
            {
                this.DisposeDriver();
                if (result.Errors.Count > 0) { result.Success = false; }
            }
            return result;
        }

        private void ScrapeEvent(string name)
        {
            
        }
    }
}
