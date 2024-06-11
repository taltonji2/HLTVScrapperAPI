using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Diagnostics;

namespace HLTVScrapperAPI.Services
{
    public class EntityExistScraper : Scraper
    {

        public bool Scrape(string entity)
        {
            WebDriverWait DriverWait = this.CreateWaitDriver(3);
            Driver.Navigate().GoToUrl("https://www.hltv.org");
            IWebElement searchBox = Driver.FindElement(By.CssSelector(".navsearchinput.tt-input"));
            searchBox.SendKeys(entity);
            searchBox.Submit();
            try
            {
                IWebElement cybotCookieDialogDeclineElement = DriverWait.Until(d =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    return d.FindElement(By.Id("CybotCookiebotDialogBodyButtonDecline"));
                });

                cybotCookieDialogDeclineElement.Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            catch (NoSuchElementException e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }
            return true;
        }
    }
}
