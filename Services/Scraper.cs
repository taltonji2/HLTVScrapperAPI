using OpenQA.Selenium;
using SeleniumUndetectedChromeDriver;
using System.Diagnostics;

namespace HLTVScrapperAPI.Services
{
    public class Scraper
    {

        protected IWebDriver driver { get; set; }

        public Scraper()
        {
            string undetectedChromeDriverPath = @"C:/Users/Timothy/source/repos/HLTVScrapperAPI/bin/ChromeDriver/chromedriver.exe";
            driver = UndetectedChromeDriver.Create(driverExecutablePath: undetectedChromeDriverPath);
        }

        protected void Dispose() 
        {
            driver.Close();
            driver.Quit();
            driver.Dispose();

            //TODO: keep track of created processes and only kill pid that were created
            IEnumerable<int> chromePids = Process.GetProcessesByName("chrome").Select(p => p.Id);
            foreach (int pid in chromePids)
            {
                Process chromeProcess = Process.GetProcessById(pid);
                if (chromeProcess != null) Process.GetProcessById(pid).Kill();
                Debug.WriteLine($"Killed process {pid}");
            }
        }
    }
}
