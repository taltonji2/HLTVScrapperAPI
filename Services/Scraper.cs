using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumUndetectedChromeDriver;

namespace HLTVScrapperAPI.Services
{
    public class Scraper
    {
        private static IWebDriver CreateWebDriver()
        {
            string undetectedChromeDriverPath = @"C:/Users/Timothy/source/repos/HLTVScrapperAPI/bin/ChromeDriver/chromedriver.exe";
            
            IWebDriver driver = UndetectedChromeDriver.Create(
                driverExecutablePath: undetectedChromeDriverPath);
            
            return driver;
        }
        
        public delegate void ScrapeAction(IWebDriver driver);

        public static void Scrape(string route = "", ScrapeAction action = null)
        {
            using (IWebDriver driver = CreateWebDriver())
            {
                try
                {
                    driver.Navigate().GoToUrl($"https://www.hltv.org/{route}");
                    
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                    
                    IWebElement cybotCookieDialogDeclineElement = wait.Until(d =>
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        
                        return d.FindElement(By.Id("CybotCookiebotDialogBodyButtonDecline"));
                    });
                    
                    cybotCookieDialogDeclineElement.Click();
                    
                    Thread.Sleep(TimeSpan.FromSeconds(3));

                    action?.Invoke(driver);

                    Thread.Sleep(TimeSpan.FromSeconds(3));
                }
                catch (Exception ex) 
                {
                    throw ex;
                }
            }
        }
        
        private static Boolean IsElementClickable(WebElement element)
        {
            if (element == null) throw new NullReferenceException(nameof(element));
            return (element.Displayed && element.Enabled) ? true : false;
        }
    }
}
