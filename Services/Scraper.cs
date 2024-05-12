using HLTVScrapperAPI.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumUndetectedChromeDriver;
using System.Diagnostics;

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
        
        public delegate NestedDictionary ScrapeAction(IWebDriver driver); //TODO: Enhance with filter for time-frame of last month three months etc

        public static NestedDictionary Scrape(string route = "", ScrapeAction action = null)
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
                    
                    Thread.Sleep(TimeSpan.FromSeconds(2));

                    try
                    {
                        return action?.Invoke(driver);
                    }
                    catch (Exception ex) 
                    {
                        throw ex;
                    }
                    finally
                    {
                        driver.Close();
                        driver.Quit();
                        driver.Dispose();

                        IEnumerable<int> chromePids = Process.GetProcessesByName("chrome").Select(p => p.Id);
                        foreach (int pid in chromePids)
                        {
                            Process chromeProcess = Process.GetProcessById(pid);
                            if (chromeProcess != null) Process.GetProcessById(pid).Kill();
                        }
                    }
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
