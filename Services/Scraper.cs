using OpenQA.Selenium;
using SeleniumUndetectedChromeDriver;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HLTVScrapperAPI.Services
{
    public class Scraper
    {
        protected IWebDriver Driver { get; set; }

        List<string> ChromeProcessId { get; set; }

        public Scraper()
        {
            Driver = this.CreateDriver();
        }

        public IWebDriver CreateDriver()
        {
            string undetectedChromeDriverPath = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                undetectedChromeDriverPath = @"C:/Users/Timothy/source/repos/HLTVScrapperAPI/bin/ChromeDriver/chromedriver.exe";
            else
                undetectedChromeDriverPath = @"bin/ChromeDriver/chromedriver";
            return UndetectedChromeDriver.Create(driverExecutablePath: undetectedChromeDriverPath);
        }

        public bool IsExist(string entity)
        {
            IWebDriver webDriver = this.CreateDriver();
            Driver.Navigate().GoToUrl("https://www.hltv.org");
            return true;
        }

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

        protected void Dispose() 
        {
            try
            {
                Driver.Close();
                Driver.Quit();
                Driver.Dispose();

                //TODO: close all chrome processes except the main application
                IEnumerable<int> chromePids = Process.GetProcessesByName("chrome").Select(p => p.Id);
                
                foreach (int pid in chromePids)
                {
                    Process chromeProcess = Process.GetProcessById(pid);
                    if (chromeProcess != null) Process.GetProcessById(pid).Kill();
                    Debug.WriteLine($"Killed process {pid}");
                }
                Debug.WriteLine("Successfully disposed chromedriver");
            } 
            catch (Exception e)
            {
                Debug.WriteLine($"Error occured while disposing chromedriver {e.Message}");
            }
           
        }
    }
}
